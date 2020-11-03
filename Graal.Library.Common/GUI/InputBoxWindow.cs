using System;
using System.Windows.Forms;

namespace Graal.Library.Common.GUI
{
    /// <summary>
    /// Окно ввода строки
    /// </summary>
    public partial class InputBoxWindow : Form
    {
        /// <summary>
        /// Делегат для передачи введенного значения
        /// </summary>
        public Action<string> GetStr { get; set; }

        /// <summary>
        /// Функция, проверящая выполнение ограничения для символа
        /// </summary>
        public Func<char, bool> InputConstraintChecker { get; set; }

        private bool OnlyLatin(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || char.IsDigit(c);
        }


        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="onString">Отображение строки ввода</param>
        /// <param name="cancel">Отображение кнопки "Отмена"</param>
        /// <param name="no">Отображение кнопки "Нет"</param>
        /// <param name="strIn">Текст строки ввода</param>
        /// <param name="name">Имя окна</param>
        /// <param name="hint">Подсказка</param>
        public InputBoxWindow(
            bool onString = true,
            bool cancel = true,
            bool no = true,
            string strIn = "",
            string name = "Input",
            string hint = "",
            Func<char, bool> inputConstraintChecker = null)
        {
            InitializeComponent();

            InputConstraintChecker = inputConstraintChecker ?? ((c) => true);

            Txt_Text.Visible = onString;
            Btn_Cancel.Visible = cancel;
            Btn_No.Visible = no;

            if (no)
                Btn_Ok.Text = "Да";

            Text = name;
            Lbl_Hint.Text = hint;
            Txt_Text.Text = strIn;

            if (hint.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).Length > 1)
                Txt_Text.Location = new System.Drawing.Point(Txt_Text.Location.X, Txt_Text.Location.Y + 15);
        }

        /// <summary>
        /// Handles the Click event of the Btn_Ok control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Btn_Ok_Click(object sender, EventArgs e)
        {
            if (Txt_Text.Visible)
                GetStr?.Invoke(Txt_Text.Text);
            Close();
        }

        /// <summary>
        /// Handles the Click event of the Btn_Cancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Btn_Cancel_Click(object sender, EventArgs e) => Close();

        /// <summary>
        /// Handles the Click event of the Btn_No control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Btn_No_Click(object sender, EventArgs e) => Close();

        /// <summary>
        /// Handles the TextChanged event of the txt_Text control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Txt_Text_TextChanged(object sender, EventArgs e)
        {
            string oldText = Txt_Text.Text, newText = string.Empty;

            foreach (char c in oldText)
                if (InputConstraintChecker(c))
                    newText += c;

            if (newText != oldText)
                Txt_Text.Text = newText;
        }
    }
}
