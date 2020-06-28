import React, { useEffect, useState } from "react";
import { HubConnectionBuilder } from "@microsoft/signalr";
import { apiAddress } from "../constants";

const ControlChannel = ({ accessToken }) => {
  const [channelUnavailable, setChannelUnavailable] = useState(false);
  const [connectionState, setConnectionState] = useState("disconnected");
  const [slideShowDetail, setSlideShowDetail] = useState({
    slideShowEnabled: false,
    title: "",
    currentSlide: 0,
    totalSlides: 0,
  });
  const [connection] = useState(
    new HubConnectionBuilder()
      .withUrl(`${apiAddress}/hub/user`, { accessTokenFactory: () => accessToken })
      .build()
  );

  useEffect(() => {
    connection
      .start()
      .then(() => {
        setConnectionState("connected");
        return connection.invoke("GetSlideShowDetail");
      })
      .then(SlideShowDetail => {
        if (SlideShowDetail.status === 200) setSlideShowDetail(SlideShowDetail.body);
      });

    connection.on("SlideShowDetailUpdated", meta => {
      setSlideShowDetail(meta);
    });

    connection.on("ChannelEnded", () => {
      setChannelUnavailable(true);
      connection.stop();
    });
  }, []);

  const sendSlideShowCommand = async code => {
    await connection.invoke("SendSlideShowCommand", code);
  };

  if (channelUnavailable) {
    return (
      <>
        <div>The presenter is no longer hosting this Power Point remote.</div>
      </>
    );
  }

  if (!slideShowDetail.slideShowEnabled) {
    return <div>Waiting for the presenter to start the slide show.</div>;
  }

  return (
    <>
      <div>State: {connectionState} </div>
      <div>Filename: {slideShowDetail.title} </div>
      <div>
        Slide: {slideShowDetail.currentSlide} / {slideShowDetail.totalSlides}{" "}
      </div>
      <button onClick={() => sendSlideShowCommand(1)}>Click Backward</button>
      <button onClick={() => sendSlideShowCommand(0)}>Click Forward</button>
    </>
  );
};

export default ControlChannel;
