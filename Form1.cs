using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Word = Microsoft.Office.Interop.Word;

namespace laba5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void FooTXT()
        {
            for (int i = 0; i < this.MdiChildren.Length; i++)
            {
                Form2 child = (Form2)this.MdiChildren[i];
                if (ActiveMdiChild == child)
                {
                    if (saveFileDialogTXT.ShowDialog() == DialogResult.OK)
                    {
                        child.richTextBox1.SaveFile(saveFileDialogTXT.FileName, RichTextBoxStreamType.PlainText);
                        MessageBox.Show("Файл успешно сохранен!\nПуть: " + saveFileDialogTXT.FileName, "Файл сохранен", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void FooWORD()
        {
            for (int i = 0; i < this.MdiChildren.Length; i++)
            {
                Form2 child = (Form2)this.MdiChildren[i];
                if (ActiveMdiChild == child)
                {
                    if (saveFileDialogWORD.ShowDialog() == DialogResult.OK)
                    {
                        child.richTextBox1.SaveFile(saveFileDialogWORD.FileName, RichTextBoxStreamType.RichText);
                        MessageBox.Show("Файл успешно сохранен!\nПуть: " + saveFileDialogWORD.FileName, "Файл сохранен", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)//добавляет новую форму
        {
            Form2 child = new Form2();
            child.MdiParent = this;
            child.Text = "Окно " + this.MdiChildren.Length.ToString();
            child.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)//закрывает активную форму или сохраняет
        {
            if (ActiveMdiChild != null)
            {
                if (MessageBox.Show("Сохранить файл?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (MessageBox.Show("В каком формате сохранить файл?\nYes- формат (*.txt)\nNo- формат (*.RTF)", "Формат файла", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        FooTXT();
                    }
                    else 
                    { 
                        FooWORD();
                    }
                }
                else
                {
                    ActiveMdiChild.Close();
                }
            }
        }

        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e)//каскад
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void horizontalToolStripMenuItem_Click(object sender, EventArgs e)//горизонтальный
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void verticalToolStripMenuItem_Click(object sender, EventArgs e)//вертикальный
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void txtToolStripMenuItem_Click(object sender, EventArgs e) //сохранени тхт файла
        {
            try
            {
                FooTXT();
            }
            catch (FileLoadException ex)
            {
                MessageBox.Show("Ошибка создания файла!\n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void wordToolStripMenuItem_Click(object sender, EventArgs e)//сохранение ворд файла формата *.rtf
        {
            try
            {
                FooWORD();
            }
            catch (FileLoadException ex)
            {
                MessageBox.Show("Ошибка сохранения файла!\n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void filedocxToolStripMenuItem_Click(object sender, EventArgs e) //сохранить файл с раширением docx
        {
            for (int i = 0; i < this.MdiChildren.Length; i++)
            {
                Form2 child = (Form2)this.MdiChildren[i];
                if (ActiveMdiChild == child)
                {
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        object docName = saveFileDialog1.FileName;
                        Word.Application application = new Microsoft.Office.Interop.Word.Application();
                        Word.Document document;
                        application.Visible = false;
                        document = application.Documents.Add();
                        child.richTextBox1.SelectAll();
                        child.richTextBox1.Copy();
                        document.Paragraphs[1].Range.Paste();
                        application.Application.ActiveDocument.SaveAs(docName);
                        application.Quit();
                        MessageBox.Show("Файл успешно сохранен!\nПуть: " + saveFileDialog1.FileName, "Файл сохранен", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) //изменение шрифта
        {
            Form2 child = (Form2)ActiveMdiChild;
            fontDialog1.Font = child.richTextBox1.SelectionFont;
            if (fontDialog1.ShowDialog() == DialogResult.OK)
                child.richTextBox1.SelectionFont = fontDialog1.Font;
        }

        private void button2_Click(object sender, EventArgs e) //изменение цвета текста
        {
            Form2 child = (Form2)this.ActiveMdiChild;
            colorDialog1.Color = child.richTextBox1.SelectionColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                child.richTextBox1.SelectionColor = colorDialog1.Color;
        }

        private void filetxtToolStripMenuItem_Click(object sender, EventArgs e) // открыть файл тхт
        {
            Form2 child = new Form2();
            child.MdiParent = this;
            child.Text = "Окно " + this.MdiChildren.Length.ToString();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(openFileDialog1.FileName, Encoding.UTF8))
                    {
                        child.richTextBox1.Text = sr.ReadToEnd();
                        sr.Close();
                    }
                }
                catch (FileLoadException ex)
                {
                    MessageBox.Show("Ошибка загрузки файла!\n" + ex.Message, "ERROR", MessageBoxButtons.OK);
                }
            }
            child.Show();
        }

        private void filedocxToolStripMenuItem1_Click(object sender, EventArgs e) // открыть вордовский файл
        {
            Form2 child = new Form2();
            child.MdiParent = this;
            child.Text = "Окно " + this.MdiChildren.Length.ToString();
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Word.Application application = new Microsoft.Office.Interop.Word.Application();
                    Word.Document doc;
                    object docName = openFileDialog2.FileName;
                    object readOnly = false;
                    object visible = true;
                    doc = application.Documents.Open(ref docName, ref readOnly, ref visible);
                    doc.ActiveWindow.Selection.WholeStory();
                    doc.ActiveWindow.Selection.Copy();
                    IDataObject dataObject = Clipboard.GetDataObject();
                    child.richTextBox1.Rtf = dataObject.GetData(DataFormats.Rtf).ToString();
                    application.Quit();
                }
                catch (FileLoadException ex)
                {
                    MessageBox.Show("Ошибка загрузки файла!\n" + ex.Message, "ERROR", MessageBoxButtons.OK);
                }
            }
            child.Show();
        }
    }

}
