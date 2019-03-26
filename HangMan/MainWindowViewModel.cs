using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Commands;
using Prism.Mvvm;

namespace HangMan
{
    public class MainWindowViewModel: BindableBase
    {
        private string word = "AISLING"; //Dream
        private int errors = 0;

        public DelegateCommand<string> CheckLetterCommand { get; set; }

        private ObservableCollection<string> secretWord;

        public ObservableCollection<string> SecretWord
        {
            get { return secretWord; }
            set { SetProperty(ref secretWord, value); }
        }

        private string hangmanImage;

        public string HangmanImage
        {
            get { return hangmanImage; }
            set { SetProperty(ref hangmanImage, value); }
        }

        public MainWindowViewModel()
        {
            SecretWord = new ObservableCollection<string>();
            foreach (var letter in word)
            {
                SecretWord.Add("?");
            }

            HangmanImage = @".\images\0.png";
            CheckLetterCommand = new DelegateCommand<string>(CheckLetter);
        }

        private void CheckLetter(string letter)
        {
            var found = false;
            for (int i = 0; i < word.Length; i++)
            {
                if (word[i].ToString() == letter)
                {
                    found = true;
                    SecretWord[i] = letter;
                }
            }

            if (!found)
            {
                errors += 1;
                HangmanImage = $".\\images\\{errors}.png";
                if (errors == 6)
                {
                    MessageBox.Show("You Lose!!!", "Nooo!!!");
                    ResetGame();
                }
            }
            else
            {
                if (SecretWord.All(x => x != "?"))
                {
                    MessageBox.Show("You Win!!!", "Yes!!!");
                    ResetGame();
                }
            }
        }

        private void ResetGame()
        {
            errors = 0;
            HangmanImage = @".\images\0.png";
            SecretWord = new ObservableCollection<string>();
            foreach (var letter in word)
            {
                SecretWord.Add("?");
            }

        }
    }
}
