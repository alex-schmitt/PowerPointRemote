import React, { useState } from "react";
import { useParams } from "react-router-dom";
import ControlChannel from "./ControlChannel";
import JoinChannel from "./JoinChannel";

const Channel = () => {
  const { channelId } = useParams();

  const [accessToken, setAccessToken] = useState(localStorage.getItem(channelId.toUpperCase()));

  if (accessToken) return <ControlChannel accessToken={accessToken} />;

  return <JoinChannel setAccessToken={setAccessToken} channelId={channelId} />;
};

export default Channel;
