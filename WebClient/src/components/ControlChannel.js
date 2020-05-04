import React, { useEffect, useState } from "react";
import { HubConnectionBuilder } from "@microsoft/signalr";

const ControlChannel = ({ accessToken }) => {
  const [connectionState, setConnectionState] = useState("disconnected");
  const [connection] = useState(
    new HubConnectionBuilder()
      .withUrl("https://localhost:5001/hub/user", { accessTokenFactory: () => accessToken })
      .build()
  );

  useEffect(() => {
    connection.start().then(() => {
      setConnectionState("connected");
    });
  }, []);

  const sendSlideShowCommand = async code => {
    await connection.invoke("SendSlideShowCommand", code)
  }

  return (
    <>
      <div>{connectionState} </div>
      <button onClick={() => sendSlideShowCommand(1)}>Previous Slide</button>
      <button onClick={() => sendSlideShowCommand(0)}>Next Slide</button>
    </>
  );
};

export default ControlChannel;
