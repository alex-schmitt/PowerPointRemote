export const clientMethod = {
  HostDisconnected: "HostDisconnected",
  HostConnected: "HostConnected",
  ChannelEnded: "ChannelEnded",
  SlideShowStarted: "SlideShowStarted",
  SlideShowEnded: "SlideShowEnded",
  SlideCountUpdated: "SlideCountUpdated",
  CurrentSlideNumberUpdated: "CurrentSlideNumberUpdated",
};

export const serverMethod = {
  sendSlideShowCommand: "SendSlideShowCommand",
  getSlideShowState: "GetSlideShowState",
  getChannelState: "GetChannelState",
};
