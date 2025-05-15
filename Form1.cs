using System;
using System.Windows.Forms;
using System.ComponentModel;

namespace HomeTheaterApp
{
    public partial class Form1 : Form
    {
        private Label lblMode; // Метка для выбора режима
        private ComboBox cmbMode; // Выпадающий список режимов
        private Button btnStartMode; // Кнопка для начала режима
        private Button btnEndMode; // Кнопка для завершения режима
        private Button btnCheckStatus; // Кнопка для проверки статуса
        private TextBox txtOutput; // Поле для вывода логов
        private HomeTheaterFacade _homeTheater; // Экземпляр фасада
        private Projector _projector; // Экземпляр проектора
        private Amplifier _amplifier; // Экземпляр усилителя
        private CdPlayer _cdPlayer; // Экземпляр CD-плеера
        private Screen _screen; // Экземпляр экрана
        private Lights _lights; // Экземпляр освещения
        private IContainer components = null; // Контейнер для компонентов формы

        public Form1()
        {
            components = new Container(); // Инициализация контейнера
            InitializeComponents();
            // Инициализация подсистем
            _projector = new Projector();
            _amplifier = new Amplifier();
            _cdPlayer = new CdPlayer();
            _screen = new Screen();
            _lights = new Lights();
            // Установка начальной стратегии (Просмотр фильма)
            UpdateStrategy();
            _homeTheater = new HomeTheaterFacade(GetCurrentStrategy());
        }

        private ITheaterModeStrategy GetCurrentStrategy()
        {
            if (cmbMode.SelectedItem.ToString() == "Просмотр фильма")
            {
                return new MovieModeStrategy(
                    level => _lights.Dim(level), () => _lights.On(), // Используем LightsToggleAction
                    () => _screen.Down(), () => _screen.Up(),
                    () => _projector.On(), () => _projector.Off(), input => _projector.SetInput(input),
                    () => _amplifier.On(), () => _amplifier.Off(), level => _amplifier.SetVolume(level),
                    () => _cdPlayer.On(), () => _cdPlayer.Off(), () => _cdPlayer.Play(), () => _cdPlayer.Stop());
            }
            else // Музыкальный режим
            {
                return new MusicModeStrategy(
                    level => _lights.Dim(level), () => _lights.On(), // Используем LightsToggleAction
                    () => _amplifier.On(), () => _amplifier.Off(), level => _amplifier.SetVolume(level),
                    () => _cdPlayer.On(), () => _cdPlayer.Off(), () => _cdPlayer.Play(), () => _cdPlayer.Stop());
            }
        }

        private void UpdateStrategy()
        {
            _homeTheater?.SetStrategy(GetCurrentStrategy());
        }

        private void InitializeComponents()
        {
            // Настройка формы
            this.Text = "Домашний кинотеатр";
            this.Size = new System.Drawing.Size(500, 450);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Метка для выбора режима
            lblMode = new Label
            {
                Text = "Выберите режим:",
                Location = new System.Drawing.Point(20, 20),
                Size = new System.Drawing.Size(100, 20)
            };

            // Выпадающий список режимов
            cmbMode = new ComboBox
            {
                Location = new System.Drawing.Point(130, 20),
                Size = new System.Drawing.Size(150, 20),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbMode.Items.AddRange(new string[] { "Просмотр фильма", "Музыкальный режим" });
            cmbMode.SelectedIndex = 0;
            cmbMode.SelectedIndexChanged += (s, e) =>
            {
                UpdateStrategy();
                txtOutput.AppendText($"Режим изменён на: {cmbMode.SelectedItem}\r\n");
            };

            // Кнопка для начала режима
            btnStartMode = new Button
            {
                Text = "Начать режим",
                Location = new System.Drawing.Point(20, 60),
                Size = new System.Drawing.Size(140, 30)
            };
            btnStartMode.Click += BtnStartMode_Click;

            // Кнопка для завершения режима
            btnEndMode = new Button
            {
                Text = "Завершить режим",
                Location = new System.Drawing.Point(170, 60),
                Size = new System.Drawing.Size(140, 30)
            };
            btnEndMode.Click += BtnEndMode_Click;

            // Кнопка для проверки статуса
            btnCheckStatus = new Button
            {
                Text = "Проверить статус",
                Location = new System.Drawing.Point(320, 60),
                Size = new System.Drawing.Size(140, 30)
            };
            btnCheckStatus.Click += BtnCheckStatus_Click;

            // Поле для вывода логов
            txtOutput = new TextBox
            {
                Location = new System.Drawing.Point(20, 100),
                Size = new System.Drawing.Size(440, 300),
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical
            };

            this.Controls.AddRange(new Control[] { lblMode, cmbMode, btnStartMode, btnEndMode, btnCheckStatus, txtOutput });
        }

        private void BtnStartMode_Click(object sender, EventArgs e)
        {
            try
            {
                string log = _homeTheater.StartMode();
                txtOutput.AppendText(log + "\r\n");
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEndMode_Click(object sender, EventArgs e)
        {
            try
            {
                string log = _homeTheater.EndMode();
                txtOutput.AppendText(log + "\r\n");
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCheckStatus_Click(object sender, EventArgs e)
        {
            try
            {
                string status = _homeTheater.GetStatus();
                txtOutput.AppendText(status + "\r\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }
    }
}