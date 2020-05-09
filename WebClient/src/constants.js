const isDev = !process.env.NODE_ENV || process.env.NODE_ENV === "development";

export const apiAddress = isDev ? "https://localhost:5001" : "https://api.ppremote.com";
