//------------------------------------------------------------------------------
// <auto-generated>
//     此代码是根据模板生成的。
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，则所做更改将丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;

namespace TZHSWEET.Entity
{
    [DataContract(IsReference = true)]
    public partial class tbOperateLog: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region 基元属性
    
        [DataMember]
        public long ID
        {
            get { return _iD; }
            set
            {
                if (_iD != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("属性“ID”是对象键的一部分，不可更改。仅当未跟踪对象或对象处于“已添加”状态时，才能对键属性进行更改。");
                    }
                    _iD = value;
                    OnPropertyChanged("ID");
                }
            }
        }
        private long _iD;
    
        [DataMember]
        public string ProcessName
        {
            get { return _processName; }
            set
            {
                if (_processName != value)
                {
                    _processName = value;
                    OnPropertyChanged("ProcessName");
                }
            }
        }
        private string _processName;
    
        [DataMember]
        public string ProcessDesc
        {
            get { return _processDesc; }
            set
            {
                if (_processDesc != value)
                {
                    _processDesc = value;
                    OnPropertyChanged("ProcessDesc");
                }
            }
        }
        private string _processDesc;
    
        [DataMember]
        public string MethodName
        {
            get { return _methodName; }
            set
            {
                if (_methodName != value)
                {
                    _methodName = value;
                    OnPropertyChanged("MethodName");
                }
            }
        }
        private string _methodName;
    
        [DataMember]
        public Nullable<int> UserID
        {
            get { return _userID; }
            set
            {
                if (_userID != value)
                {
                    _userID = value;
                    OnPropertyChanged("UserID");
                }
            }
        }
        private Nullable<int> _userID;
    
        [DataMember]
        public string UserName
        {
            get { return _userName; }
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    OnPropertyChanged("UserName");
                }
            }
        }
        private string _userName;
    
        [DataMember]
        public string IPAddress
        {
            get { return _iPAddress; }
            set
            {
                if (_iPAddress != value)
                {
                    _iPAddress = value;
                    OnPropertyChanged("IPAddress");
                }
            }
        }
        private string _iPAddress;
    
        [DataMember]
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged("Description");
                }
            }
        }
        private string _description;
    
        [DataMember]
        public Nullable<System.DateTime> CreateDate
        {
            get { return _createDate; }
            set
            {
                if (_createDate != value)
                {
                    _createDate = value;
                    OnPropertyChanged("CreateDate");
                }
            }
        }
        private Nullable<System.DateTime> _createDate;
    
        [DataMember]
        public string MethodDesc
        {
            get { return _methodDesc; }
            set
            {
                if (_methodDesc != value)
                {
                    _methodDesc = value;
                    OnPropertyChanged("MethodDesc");
                }
            }
        }
        private string _methodDesc;

        #endregion
        #region ChangeTracking
    
        protected virtual void OnPropertyChanged(String propertyName)
        {
            if (ChangeTracker.State != ObjectState.Added && ChangeTracker.State != ObjectState.Deleted)
            {
                ChangeTracker.State = ObjectState.Modified;
            }
            if (_propertyChanged != null)
            {
                _propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    
        protected virtual void OnNavigationPropertyChanged(String propertyName)
        {
            if (_propertyChanged != null)
            {
                _propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged{ add { _propertyChanged += value; } remove { _propertyChanged -= value; } }
        private event PropertyChangedEventHandler _propertyChanged;
        private ObjectChangeTracker _changeTracker;
    
        [DataMember]
        public ObjectChangeTracker ChangeTracker
        {
            get
            {
                if (_changeTracker == null)
                {
                    _changeTracker = new ObjectChangeTracker();
                    _changeTracker.ObjectStateChanging += HandleObjectStateChanging;
                }
                return _changeTracker;
            }
            set
            {
                if(_changeTracker != null)
                {
                    _changeTracker.ObjectStateChanging -= HandleObjectStateChanging;
                }
                _changeTracker = value;
                if(_changeTracker != null)
                {
                    _changeTracker.ObjectStateChanging += HandleObjectStateChanging;
                }
            }
        }
    
        private void HandleObjectStateChanging(object sender, ObjectStateChangingEventArgs e)
        {
            if (e.NewState == ObjectState.Deleted)
            {
                ClearNavigationProperties();
            }
        }
    
        protected bool IsDeserializing { get; private set; }
    
        [OnDeserializing]
        public void OnDeserializingMethod(StreamingContext context)
        {
            IsDeserializing = true;
        }
    
        [OnDeserialized]
        public void OnDeserializedMethod(StreamingContext context)
        {
            IsDeserializing = false;
            ChangeTracker.ChangeTrackingEnabled = true;
        }
    
        protected virtual void ClearNavigationProperties()
        {
        }

        #endregion
    }
}
