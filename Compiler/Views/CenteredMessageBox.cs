using System;
using System.Drawing;
using System.Windows.Forms;

public static class CenteredMessageBox
{
    public static DialogResult Show(Form owner, string text, string caption,
        MessageBoxButtons buttons = MessageBoxButtons.OK,
        MessageBoxIcon icon = MessageBoxIcon.None)
    {
        using (Form prompt = new Form())
        {
            prompt.Width = 400;
            prompt.FormBorderStyle = FormBorderStyle.FixedDialog;
            prompt.Text = caption;
            prompt.StartPosition = FormStartPosition.CenterParent;
            prompt.MaximizeBox = false;
            prompt.MinimizeBox = false;
            prompt.ShowIcon = false;
            prompt.ShowInTaskbar = false;

            PictureBox pictureBox = new PictureBox();
            if (icon != MessageBoxIcon.None)
            {
                Icon sysIcon = GetSystemIcon(icon);
                if (sysIcon != null)
                {
                    pictureBox.Image = sysIcon.ToBitmap();
                    pictureBox.Location = new Point(20, 20);
                    pictureBox.Size = new Size(32, 32);
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    prompt.Controls.Add(pictureBox);
                }
            }

            Label textLabel = new Label();
            textLabel.Text = text;
            textLabel.Location = new Point(icon == MessageBoxIcon.None ? 20 : 65, 25);
            textLabel.MaximumSize = new Size(icon == MessageBoxIcon.None ? 340 : 300, 0);
            textLabel.AutoSize = true;
            prompt.Controls.Add(textLabel);

            FlowLayoutPanel buttonPanel = new FlowLayoutPanel();
            buttonPanel.FlowDirection = FlowDirection.RightToLeft;
            buttonPanel.Dock = DockStyle.Bottom;
            buttonPanel.Height = 50;
            buttonPanel.Padding = new Padding(10, 10, 10, 0);
            prompt.Controls.Add(buttonPanel);

            AddButtons(prompt, buttonPanel, buttons);

            prompt.Height = textLabel.Bottom + buttonPanel.Height + 50;

            System.Media.SystemSounds.Beep.Play();

            return prompt.ShowDialog(owner);
        }
    }

    private static Icon? GetSystemIcon(MessageBoxIcon icon)
    {
        return icon switch
        {
            MessageBoxIcon.Error => SystemIcons.Error,
            MessageBoxIcon.Information => SystemIcons.Information,
            MessageBoxIcon.Question => SystemIcons.Question,
            MessageBoxIcon.Warning => SystemIcons.Warning,
            _ => null
        };
    }

    private static void AddButtons(Form prompt, FlowLayoutPanel panel, MessageBoxButtons buttons)
    {
        switch (buttons)
        {
            case MessageBoxButtons.OK:
                CreateButton(prompt, panel, "ОК", DialogResult.OK, true);
                break;
            case MessageBoxButtons.OKCancel:
                CreateButton(prompt, panel, "Отмена", DialogResult.Cancel);
                CreateButton(prompt, panel, "ОК", DialogResult.OK, true);
                break;
            case MessageBoxButtons.YesNo:
                CreateButton(prompt, panel, "Нет", DialogResult.No);
                CreateButton(prompt, panel, "Да", DialogResult.Yes, true);
                break;
            case MessageBoxButtons.YesNoCancel:
                CreateButton(prompt, panel, "Отмена", DialogResult.Cancel);
                CreateButton(prompt, panel, "Нет", DialogResult.No);
                CreateButton(prompt, panel, "Да", DialogResult.Yes, true);
                break;
        }
    }

    private static void CreateButton(Form prompt, FlowLayoutPanel panel, string text, DialogResult result, bool isDefault = false)
    {
        Button btn = new Button
        {
            Text = text,
            DialogResult = result,
            Width = 80,
            Height = 28,
            Margin = new Padding(5, 0, 0, 0)
        };

        panel.Controls.Add(btn);

        if (isDefault)
        {
            prompt.AcceptButton = btn;
        }
        if (result == DialogResult.Cancel)
        {
            prompt.CancelButton = btn;
        }
    }
}