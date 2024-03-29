﻿using MultiTranslator.Data;
using MultiTranslator.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiTranslator.Pages
{
    public class TranslatorBase : ComponentBase
    {
        [Inject]
        protected TranslationService translationService { get; set; }

        protected string inputLanguage { get; set; } = default;
        protected string outputLanguage { get; set; } = default;
        protected string inputText { get; set; } = default;
        protected string outputText { get; set; } = default;
        protected Dictionary<string, LanguageDetails> LanguageList = new Dictionary<string, LanguageDetails>();

        protected override async Task OnInitializedAsync()
        {
            AvailableLanguage availableLanguages = await translationService.GetAvailableLanguages();
            LanguageList = availableLanguages.Translation;
        }

        protected void SelectLanguage(ChangeEventArgs languageEvent)
        {
            outputLanguage = languageEvent.Value.ToString();
        }

        protected async Task Translate()
        {
            if (!string.IsNullOrEmpty(outputLanguage))
            {
                TranslationResult[] translations = await translationService.GetTranslatation(inputText, outputLanguage);

                if (translations != null)
                {
                    outputText = translations[0].Translations[0].Text;
                    inputLanguage = translations[0].DetectedLanguage.Language;
                }
                else
                {
                    outputText = "An error occurred in translation. Please try again.";
                }
            }
        }

        protected async Task<string> Translate(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                TranslationResult[] translations = await translationService.GetTranslatation(text, outputLanguage);

                if (translations != null)
                {
                    outputText = translations[0].Translations[0].Text;
                    inputLanguage = translations[0].DetectedLanguage.Language;
                }
                else
                {
                    outputText = "An error occurred in translation. Please try again.";
                }
            }
            return outputText;
        }
    }
}
