import React, { useState } from "react";

const JoinChannel = ({ setAccessToken, channelId }) => {
  const [errorMessage, setErrorMessage] = useState("");
  const [userName, setUserName] = useState("");

  const submit = async event => {
    event.preventDefault();

    const response = await fetch("https://localhost:5001/join-channel", {
      method: "post",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ userName, channelId }),
    });

    switch (response.status) {
      case 200: {
        const { accessToken } = await response.json();
        localStorage.setItem(channelId.toUpperCase(), accessToken);
        setAccessToken(accessToken);
        break;
      }
      case 404:
        setErrorMessage("The session ended or doesn't exist");
        break;
      default:
        setErrorMessage("Error");
    }
  };

  return (
    <>
      <form onSubmit={submit}>
        <input
          type="name"
          placeholder="Name"
          onChange={event => {
            setUserName(event.target.value);
          }}
        />
        <input type="submit" />
      </form>
      {errorMessage && <div>{errorMessage}</div>}
    </>
  );
};

export default JoinChannel;
