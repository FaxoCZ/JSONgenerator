
using JSONgenerator.Helpers;
using System.Data;

namespace JSONgenerator.Presentation.Components
{
    public class TextBoxEnter : BaseComponent
    {
        public override bool Selectable => true;

        public event Action? Clicked;
        public event Action? Deleted;
        public string Value { get; set; }

        private string _text;
        private int _size;

        public TextBoxEnter(string text, int size = 32, bool inline = false)
            : base(inline)
        {
            Value = string.Empty;
            _text = text;
            _size = size;
        }

        public override void Render(bool selected)
        {
            char pad = selected ? '_' : ' ';
            string content = Value.PadRight(_size, pad);

            ConsoleHelper.WriteConditionalColor($"{_text}{content}", selected, ConsoleColor.Red);

            base.Render(selected);
        }

        public override void HandleKey(ConsoleKeyInfo keyInfo)
        {
            if (keyInfo.Key == ConsoleKey.Enter)
                Clicked?.Invoke();
            if(keyInfo.Key == ConsoleKey.Delete)
                Deleted?.Invoke();  
        }
    }
}
