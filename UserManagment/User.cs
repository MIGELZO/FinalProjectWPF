using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FinalProjectWPF.UserManagment
{
    class User : INotifyPropertyChanged
    {
        private string _fullName;
        private int _userId;
        private DateTime _lastLogin;
        private bool _isLogin;
        public event PropertyChangedEventHandler? PropertyChanged;

        public string FullName
        {
            get
            {
                return _fullName;
            }
            set
            {
                _fullName = value;
                OnPropertyChanged();
            }
        }
        public int UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                _userId = value;
            }
        }
        public DateTime LastLogin
        {
            get
            {
                return _lastLogin;
            }
            set
            {
                _lastLogin = value;
            }
        }

        public bool IsLogin
        {
            get
            {
                return _isLogin;
            }
            set
            {
                _isLogin = value;
            }
        }

        public User(string fullName, int userId)
        {
            FullName = fullName;
            UserId = userId;
            LastLogin = DateTime.Now;
            IsLogin = true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
