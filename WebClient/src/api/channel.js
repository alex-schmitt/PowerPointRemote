import axios from "axios";
import { apiAddress } from "../constants";

const webapi = axios.create({ baseURL: apiAddress });

const joinChannel = (userName, channelId) => webapi.post("/join-channel", { userName, channelId });

export const channelApi = { joinChannel };
