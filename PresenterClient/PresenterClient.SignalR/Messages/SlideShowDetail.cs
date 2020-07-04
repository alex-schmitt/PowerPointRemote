﻿using System;

namespace PresenterClient.SignalR.Messages
{
    public class SlideShowDetail
    {
        public bool SlideShowEnabled { get; set; }
        public string Name { get; set; }
        public int CurrentSlide { get; set; }
        public int TotalSlides { get; set; }
        public DateTime Timestamp { get; set; }
    }
}