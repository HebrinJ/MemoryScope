using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace MemoryScope
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string itemName;            //Название элемента пользователя
        string itemType;            //Тип выбранного пользователем элемента
        byte itemTypePosition;      //Расположение: 0 - stack, 1 - heap
        string outputValueDop = ""; //Дополнительная переменная для выведения корректной записи в стек при составных типах данных

        TextBlock[] blockLeft;      //Массив значений в стек
        TextBlock[] blockRight;     //Массив значений в хип
        TextBlock[] mainBlock1;     //Массив записей 1 верхнего блока
        TextBlock[] mainBlock2;     //Массив записей 2 верхнего блока
        //Button[] buttons;           //Массив кнопок
        TextBlock[] mainBlock3;     //Массив записей 3 верхнего блока

        int whatButton = 0;         //Переменная определяет какой кнопкой добавлено дополнительное значение. 0 - общая кнопка
        string outputValue;         //Переменная для вывода текста


        public MainWindow()
        {
            InitializeComponent();

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            blockLeft = new TextBlock[] { S1, S2, S3, S4, S5, S6, S7 };
            blockRight = new TextBlock[] { H1, H1, H3, H4, H5, H6, H7 };
            mainBlock1 = new TextBlock[] { El1, El2, El3, El4, El5, El6, El7 };
            mainBlock2 = new TextBlock[] { El8, El9, El10, El11, El12, El13, El14 };
            mainBlock3 = new TextBlock[] { El15, El16, El17, El18, El19, El20, El21 };
            //buttons = new Button[] { AddItem, Add1, Add2, Add3, Add4, Add5, Add6, Add7, Add8, Add9, Add10, Add11, Add13, Add14 };


        }


        private void AddItem_Click(object sender, RoutedEventArgs e )    //Активируем панель выбора элемента
        {
            InputPanel.IsEnabled = true;
            InputPanel.Visibility = Visibility.Visible;
            
            Button sendNameO = (Button)sender;
            string sendName = sendNameO.Name.ToString();
            sendName = sendName.Substring(3);                           //Узнаем какую кнопку нажал юзер
            if (sendName == "Item")
                whatButton = 0;                                         //Юзер нажал главную
            else
                whatButton = Convert.ToInt32(sendName);                 //Юзер нажал одну из дополнительных. Получаем индекс.
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)     //Проверяем какая радиокнопка выбрана
        {

            if (NameByte.IsChecked == true)
            {
                itemType = "byte(short)";
                itemTypePosition = 0;
            }
            else if (NameObject.IsChecked == true)
            {
                itemType = "object";
                itemTypePosition = 1;
            }
            else if (NameInt.IsChecked == true)
            {
                itemType = "int(long)";
                itemTypePosition = 0;
            }
            else if (NameString.IsChecked == true)
            {
                itemType = "string";
                itemTypePosition = 1;
            }
            else if (NameFloat.IsChecked == true)
            {
                itemType = "float(double)";
                itemTypePosition = 0;
            }
            else if (NameClass.IsChecked == true)
            {
                itemType = "class(Ie)";
                itemTypePosition = 1;
            }
            else if (NameDec.IsChecked == true)
            {
                itemType = "char(bool,dec)";
                itemTypePosition = 0;
            }
            else if (NameDelegate.IsChecked == true)
            {
                itemType = "delegate";
                itemTypePosition = 1;
            }
            else if (NameEnum.IsChecked == true)
            {
                itemType = "struct(enum)";
                itemTypePosition = 0;
            }
            else
            {
                MessageBox.Show("Не выбран не один тип");
            }


            AddNamePanel.Visibility = Visibility.Visible;       //Активируем панель ввода названия переменной

        }

        private void AddName_Click(object sender, RoutedEventArgs e)     //Определение куда записывать данные. В стек или хип.
        {
            itemName = NameField.Text;
            outputValue = $"{itemType} {itemName}";

            if (whatButton != 0)                         //Если значений добавлено не основной кнопкой пересчитываем тип значения опираясь на предыдущее
            {
                CountMassive(mainBlock1);
            }
            

            if (itemTypePosition == 0)                   //Значение для стека                
            {
                EnterBlock(blockLeft);
                
            }
            
            else if (itemTypePosition == 1)             //Значение для хипа
            {
                EnterBlock(blockRight);

                foreach (TextBlock i in blockLeft)                      //Если данные в хипе, создаем запись со ссылкой в стеке
                {
                    if ((string)i.Text == "")
                    {
                        if (whatButton == 0)
                        {
                            i.Text = $"Ссылка на: {outputValue}";
                            break;
                        }
                        else 
                        {
                            i.Text = $"Ссылка на: {outputValueDop}";
                            break;
                        }
                        
                    }
                }
            }

        }

        

        private string EnterBlock(TextBlock[] textMassive)    //Определение блока для отображение данных и активации кнопок
        {
            
            foreach (TextBlock i in textMassive)
            {
                if ((string)i.Text == "")
                {
                   
                    
                    InputPanel.IsEnabled = false;
                    InputPanel.Visibility = Visibility.Hidden;
                    AddNamePanel.Visibility = Visibility.Hidden;
                    if (whatButton == 0)                               //Если нажата главная кнопка
                    {
                        i.Text = outputValue;
                        foreach (TextBlock j in mainBlock1)
                        {
                            if ((string)j.Text == "")               //Определяется текстовое поле для записи
                            {
                                j.Text = outputValue;
                                if (j.Name == "El1" && (itemType == "struct(enum)" || itemType == "class(Ie)" || itemType == "delegate" || itemType == "object"))
                                {
                                    Add1.IsEnabled = true;
                                    
                                }
                                else if (j.Name == "El2" && (itemType == "struct(enum)" || itemType == "class(Ie)" || itemType == "delegate" || itemType == "object"))
                                {
                                    Add2.IsEnabled = true;
                                    
                                }
                                else if (j.Name == "El3" && (itemType == "struct(enum)" || itemType == "class(Ie)" || itemType == "delegate" || itemType == "object"))
                                {
                                    Add3.IsEnabled = true;
                                    
                                }
                                else if (j.Name == "El4" && (itemType == "struct(enum)" || itemType == "class(Ie)" || itemType == "delegate" || itemType == "object"))
                                {
                                    Add4.IsEnabled = true;
                                    
                                }
                                else if (j.Name == "El5" && (itemType == "struct(enum)" || itemType == "class(Ie)" || itemType == "delegate" || itemType == "object"))
                                {
                                    Add5.IsEnabled = true;
                                    
                                }
                                else if (j.Name == "El6" && (itemType == "struct(enum)" || itemType == "class(Ie)" || itemType == "delegate" || itemType == "object"))
                                {
                                    Add6.IsEnabled = true;
                                    
                                }
                                else if (j.Name == "El7" && (itemType == "struct(enum)" || itemType == "class(Ie)" || itemType == "delegate" || itemType == "object"))
                                {
                                    Add7.IsEnabled = true;
                                    
                                }
                                outputValueDop = "";
                                
                                break;
                            }
                            


                        }
                        break;
                    }
                    else if (whatButton > 0 && whatButton <= 7)     //Если нажата кнопка первого блока
                    {
                        foreach (TextBlock j in mainBlock2)
                        {
                            
                            if ((string)j.Text == "" && (whatButton == (Convert.ToInt32(j.Name.Substring(2))) - 7))     //Определяется текстовое поле для записи
                            {
                                
                                if (j.Name == "El8" && whatButton == 1)
                                {
                                    j.Text = $"{El1.Text}.{itemName}";
                                    i.Text = $"{El1.Text}.{itemName}";
                                    if (itemType == "struct(enum)" || itemType == "class(Ie)" || itemType == "delegate" || itemType == "object")
                                        Add8.IsEnabled = true;
                                }
                                else if (j.Name == "El9" && whatButton == 2)
                                {
                                    j.Text = $"{El2.Text}.{itemName}";
                                    i.Text = $"{El2.Text}.{itemName}";
                                    if (itemType == "struct(enum)" || itemType == "class(Ie)" || itemType == "delegate" || itemType == "object")
                                        Add9.IsEnabled = true;    
                                }
                                else if (j.Name == "El10" && whatButton == 3)
                                {
                                    j.Text = $"{El3.Text}.{itemName}";
                                    i.Text = $"{El3.Text}.{itemName}";
                                    if (itemType == "struct(enum)" || itemType == "class(Ie)" || itemType == "delegate" || itemType == "object")
                                        Add10.IsEnabled = true;    
                                }
                                else if (j.Name == "El11" && whatButton == 4)
                                {
                                    j.Text = $"{El4.Text}.{itemName}";
                                    i.Text = $"{El4.Text}.{itemName}";
                                    if (itemType == "struct(enum)" || itemType == "class(Ie)" || itemType == "delegate" || itemType == "object")
                                        Add11.IsEnabled = true;    
                                }
                                else if (j.Name == "El12" && whatButton == 5)
                                {
                                    j.Text = $"{El5.Text}.{itemName}";
                                    i.Text = $"{El5.Text}.{itemName}";
                                    if (itemType == "struct(enum)" || itemType == "class(Ie)" || itemType == "delegate" || itemType == "object")
                                        Add12.IsEnabled = true;    
                                }
                                else if (j.Name == "El13" && whatButton == 6)
                                {
                                    j.Text = $"{El6.Text}.{itemName}";
                                    i.Text = $"{El6.Text}.{itemName}";
                                    if (itemType == "struct(enum)" || itemType == "class(Ie)" || itemType == "delegate" || itemType == "object")
                                        Add13.IsEnabled = true;    
                                }
                                else if (j.Name == "El14" && whatButton == 7)
                                {
                                    j.Text = $"{El7.Text}.{itemName}";
                                    i.Text = $"{El7.Text}.{itemName}";
                                    if (itemType == "struct(enum)" || itemType == "class(Ie)" || itemType == "delegate" || itemType == "object")
                                        Add14.IsEnabled = true;    
                                }
                                outputValueDop = j.Text;
                                
                                break;
                            }



                        }
                        
                        break;
                    }
                    else if (whatButton > 7)        //Если нажата кнопка второго блока
                    {
                        foreach (TextBlock j in mainBlock3)
                        {

                            if ((string)j.Text == "" && (whatButton == (Convert.ToInt32(j.Name.Substring(2))) - 7))     //Определяется текстовое поле для записи
                            {

                                if (j.Name == "El15" && whatButton == 8)
                                {
                                    i.Text = $"{El8.Text}.{itemName}";
                                    j.Text = $"{El8.Text}.{itemName}";
                                }
                                else if (j.Name == "El16" && whatButton == 9)
                                {
                                    i.Text = $"{El9.Text}.{itemName}";
                                    j.Text = $"{El9.Text}.{itemName}";
                                }
                                else if (j.Name == "El17" && whatButton == 10)
                                {
                                    i.Text = $"{El10.Text}.{itemName}";
                                    j.Text = $"{El10.Text}.{itemName}";
                                }
                                else if (j.Name == "El18" && whatButton == 11)
                                {
                                    i.Text = $"{El11.Text}.{itemName}";
                                    j.Text = $"{El11.Text}.{itemName}";
                                }
                                else if (j.Name == "El19" && whatButton == 12)
                                {
                                    i.Text = $"{El12.Text}.{itemName}";
                                    j.Text = $"{El12.Text}.{itemName}";
                                }
                                else if (j.Name == "El20" && whatButton == 13)
                                {
                                    i.Text = $"{El13.Text}.{itemName}";
                                    j.Text = $"{El13.Text}.{itemName}";
                                }
                                else if (j.Name == "El21" && whatButton == 14)
                                {
                                    i.Text = $"{El14.Text}.{itemName}";
                                    j.Text = $"{El14.Text}.{itemName}";
                                }
                                outputValueDop = j.Text;
                                
                                break;
                            }



                        }
                        
                        
                        break;
                    }
                }
            
            }
        return outputValueDop;
        }

        private byte CountMassive(TextBlock[] textMassive)          //Проверка массива на предмет изменения типа в зависимости от предыдущей записи
        {
            string symb;
            foreach (TextBlock k in textMassive)
            {
                if (k.Text != "")
                {
                    symb = k.Text.Substring(0, 3);
                    if (symb == "str" || symb == "cla" || symb == "del" || symb == "obj")
                    {
                        itemTypePosition = 1;
                    }
                    else
                    {
                        itemTypePosition = 0;
                    }
                }
            }
            return itemTypePosition;
        }

    }

}

