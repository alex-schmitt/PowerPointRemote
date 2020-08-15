import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { joinChannel } from "../thunks";
import { useDispatch } from "react-redux";

const Channel = ({ channelService }) => {
  const dispatch = useDispatch();
  const { channelId } = useParams();

  useEffect(() => {
    (async () => {
      const result = await dispatch(joinChannel({ channelId, userName: "Alex" }));

      if (!result.error) {
        channelService.start(result.payload.accessToken);
      }
    })();
  }, []);

  return <div>Hello</div>;
};

export default Channel;
