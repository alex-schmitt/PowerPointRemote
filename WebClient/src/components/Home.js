import React, { useState } from "react";
import { useHistory } from "react-router-dom";

const Home = () => {
  const history = useHistory();
  const [channelId, setChannelId] = useState("");

  const submit = event => {
    event.preventDefault();
    history.push(`/${channelId}`);
  };

  return (
    <form onSubmit={submit}>
      <input
        type="text"
        onChange={event => setChannelId(event.target.value.toUpperCase())}
        value={channelId}
      />
      <input type="submit" />
    </form>
  );
};

export default Home;
