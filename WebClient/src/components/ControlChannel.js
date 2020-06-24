import React, { useEffect, useState } from "react";
import { HubConnectionBuilder } from "@microsoft/signalr";
import { apiAddress } from "../constants";

const ControlChannel = ({ accessToken }) => {
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
        if (slideShowMeta.status === 200)
          setSlideShowMeta(slideShowMeta.body);
      });

    connection.on("ReceiveSlideShowMeta", meta => {
      setSlideShowMeta(meta);
      console.log(meta);
    });
  }, []);

  const sendSlideShowCommand = async code => {
    await connection.invoke("SendSlideShowCommand", code);
  };

  return (
    <>
      <div>State: {connectionState} </div>
      <div>Filename: {slideShowMeta.title} </div>
      <div>
        Slide: {slideShowMeta.currentSlide} / {slideShowMeta.totalSlides}{" "}
      </div>
      <div>Slide Show Active: {slideShowMeta.slideShowEnabled} </div>
      <button onClick={() => sendSlideShowCommand(1)}>Previous Slide</button>
      <button onClick={() => sendSlideShowCommand(0)}>Next Slide</button>
    </>
  );
};

export default ControlChannel;
