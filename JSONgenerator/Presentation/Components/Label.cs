namespace JSONgenerator.Presentation.Components
{
    public class Label : BaseComponent
    {
        public override bool Selectable => false;

        public string Text { get; set; }

        public Label(string text, bool inline = false)
            : base(inline)
        {
            Text = text;
        }

        public override void Render(bool selected)
        {
            Console.Write(Text);

            base.Render(selected);
        }
    }
}
