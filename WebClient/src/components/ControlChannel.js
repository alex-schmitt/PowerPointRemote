import React, { useEffect, useState } from "react";
import { HubConnectionBuilder } from "@microsoft/signalr";
import { apiAddress } from "../constants";

const ControlChannel = ({ accessToken }) => {
  const [channelUnavailable, setChannelUnavailable] = useState(false);
  const [connectionState, setConnectionState] = useState("disconnected");
  const [slideShowMeta, setSlideShowMeta] = useState({
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
        return connection.invoke("GetSlideShowMeta");
      })
      .then(slideShowMeta => {
        if (slideShowMeta.status === 200) setSlideShowMeta(slideShowMeta.body);
      });

    connection.on("ReceiveSlideShowMeta", meta => {
      setSlideShowMeta(meta);
    });

    connection.on("ChannelEnded", () => {
      setChannelUnavailable(true);
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

  if (!slideShowMeta.slideShowEnabled) {
    return <div>Waiting for the presenter to start the slide show.</div>;
  }

  return (
    <>
      <div>State: {connectionState} </div>
      <div>Filename: {slideShowMeta.title} </div>
      <div>
        Slide: {slideShowMeta.currentSlide} / {slideShowMeta.totalSlides}{" "}
      </div>
      <button onClick={() => sendSlideShowCommand(1)}>Click Backward</button>
      <button onClick={() => sendSlideShowCommand(0)}>Click Forward</button>
    </>
  );
};

export default ControlChannel;
