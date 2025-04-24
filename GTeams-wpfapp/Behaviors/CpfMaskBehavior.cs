using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;
using System.Windows.Input;
using System;
using System.Windows;
using System.Linq;
using System.Collections.Generic;

namespace GTeams_wpfapp.Behaviors
{
    public class CpfMaskBehavior : Behavior<TextBox>
    {
        private bool _isUpdating = false;
        private const int MaskLength = 14;
        private static readonly int[] DotPositions = { 3, 6 };
        private const int DashPosition = 9;

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PreviewTextInput += PreviewTextInputHandler;
            AssociatedObject.TextChanged += TextChangedHandler;
            DataObject.AddPastingHandler(AssociatedObject, PastingHandler);
        }
        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PreviewTextInput -= PreviewTextInputHandler;
            AssociatedObject.TextChanged -= TextChangedHandler;
            DataObject.RemovePastingHandler(AssociatedObject, PastingHandler);
        }
        private void PreviewTextInputHandler(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsNumericInput(e.Text);
        }
        private void TextChangedHandler(object sender, TextChangedEventArgs e)
        {
            if (_isUpdating) return;
            if (sender is not TextBox textBox) return;
            _isUpdating = true;
            string numbers = new string(textBox.Text.Where(char.IsDigit).ToArray());
            string masked = ApplyCpfMask(numbers);
            int newSelectionStart = CalculateSelectionStart(textBox.Text, masked, textBox.SelectionStart);
            textBox.Text = masked;
            textBox.SelectionStart = newSelectionStart;
            _isUpdating = false;
        }

        private void PastingHandler(object sender, DataObjectPastingEventArgs e)
        {
            if (!e.DataObject.GetDataPresent(typeof(string)))
            {
                e.CancelCommand();
                return;
            }
            if (sender is not TextBox textBox)
            {
                e.CancelCommand();
                return;
            }
            string pastedText = (string)e.DataObject.GetData(typeof(string));
            string pastedDigits = new string(pastedText.Where(char.IsDigit).ToArray());
            if (string.IsNullOrEmpty(pastedDigits))
            {
                e.CancelCommand();
                return;
            }
            int selectionStart = textBox.SelectionStart;
            int selectionLength = textBox.SelectionLength;
            string currentDigits = new string(textBox.Text.Where(char.IsDigit).ToArray());
            if (selectionLength > 0 && selectionStart < textBox.Text.Length)
            {
                int removed = 0, i = 0;
                List<char> chars = new List<char>();
                foreach (char c in textBox.Text)
                {
                    if (char.IsDigit(c))
                    {
                        if (i < selectionStart || i >= selectionStart + selectionLength)
                            chars.Add(c);
                        i++;
                    }
                }
                currentDigits = new string(chars.ToArray());
            }
            string newDigits;
            if (selectionStart > 0)
            {
                int nonMaskCursor = textBox.Text.Take(selectionStart).Count(c => char.IsDigit(c));
                newDigits = currentDigits.Insert(nonMaskCursor, pastedDigits);
            }
            else
            {
                newDigits = pastedDigits + currentDigits;
            }
            string masked = ApplyCpfMask(newDigits);
            _isUpdating = true;
            textBox.Text = masked;
            int digitsUpTo = CalculateSelectionStartForPasting(masked, textBox.Text, selectionStart, pastedDigits.Length);
            textBox.SelectionStart = digitsUpTo;
            _isUpdating = false;
            e.CancelCommand();
        }

        private static string ApplyCpfMask(string digits)
        {
            var masked = "";
            int len = Math.Min(digits.Length, 11);
            for (int i = 0; i < len && masked.Length < MaskLength; i++)
            {
                if (DotPositions.Contains(i)) masked += ".";
                else if (i == DashPosition) masked += "-";
                masked += digits[i];
            }
            return masked;
        }

        private static int CalculateSelectionStart(string originalText, string maskedText, int originalSelectionStart)
        {
            int beforeCursor = 0, digitsCount = 0;
            for (int i = 0; i < maskedText.Length && digitsCount < originalSelectionStart; i++)
            {
                if (char.IsDigit(maskedText[i])) digitsCount++;
                beforeCursor++;
                if (digitsCount == originalSelectionStart) break;
            }
            if (beforeCursor < maskedText.Length && (maskedText[beforeCursor] == '.' || maskedText[beforeCursor] == '-'))
                beforeCursor++;
            return beforeCursor > maskedText.Length ? maskedText.Length : beforeCursor;
        }

        private static int CalculateSelectionStartForPasting(string masked, string originalMasked, int selectionStart, int pasteLength)
        {
            int nonMaskCursor = originalMasked.Take(selectionStart).Count(char.IsDigit) + pasteLength;
            int i = 0, digits = 0;
            for (; i < masked.Length; i++)
            {
                if (char.IsDigit(masked[i])) digits++;
                if (digits == nonMaskCursor) break;
            }
            if (i < masked.Length && (masked[i] == '.' || masked[i] == '-')) i++;
            return i > masked.Length ? masked.Length : i;
        }

        private static bool IsNumericInput(string text)
        {
            return text.All(char.IsDigit);
        }
    }
}
