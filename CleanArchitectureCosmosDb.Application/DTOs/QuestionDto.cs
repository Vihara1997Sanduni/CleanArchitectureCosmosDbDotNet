﻿namespace CleanArchitectureCosmosDb.Application.DTOs;

 public class QuestionDto
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Text { get; set; }
        public List<string> Options { get; set; }
        public int? MaxChoices { get; set; }
        public bool EnableOtherOption { get; set; }
    }

