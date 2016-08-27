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
    public partial class VRoleGrantPermission: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region 基元属性
    
        [DataMember]
        public int PermissionID
        {
            get { return _permissionID; }
            set
            {
                if (_permissionID != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("属性“PermissionID”是对象键的一部分，不可更改。仅当未跟踪对象或对象处于“已添加”状态时，才能对键属性进行更改。");
                    }
                    _permissionID = value;
                    OnPropertyChanged("PermissionID");
                }
            }
        }
        private int _permissionID;
    
        [DataMember]
        public string PermissionAction
        {
            get { return _permissionAction; }
            set
            {
                if (_permissionAction != value)
                {
                    _permissionAction = value;
                    OnPropertyChanged("PermissionAction");
                }
            }
        }
        private string _permissionAction;
    
        [DataMember]
        public string PermissionName
        {
            get { return _permissionName; }
            set
            {
                if (_permissionName != value)
                {
                    _permissionName = value;
                    OnPropertyChanged("PermissionName");
                }
            }
        }
        private string _permissionName;
    
        [DataMember]
        public string Icon
        {
            get { return _icon; }
            set
            {
                if (_icon != value)
                {
                    _icon = value;
                    OnPropertyChanged("Icon");
                }
            }
        }
        private string _icon;
    
        [DataMember]
        public string PermissionController
        {
            get { return _permissionController; }
            set
            {
                if (_permissionController != value)
                {
                    _permissionController = value;
                    OnPropertyChanged("PermissionController");
                }
            }
        }
        private string _permissionController;
    
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
        public Nullable<int> ParentID
        {
            get { return _parentID; }
            set
            {
                if (_parentID != value)
                {
                    _parentID = value;
                    OnPropertyChanged("ParentID");
                }
            }
        }
        private Nullable<int> _parentID;
    
        [DataMember]
        public string ModuleController
        {
            get { return _moduleController; }
            set
            {
                if (_moduleController != value)
                {
                    _moduleController = value;
                    OnPropertyChanged("ModuleController");
                }
            }
        }
        private string _moduleController;
    
        [DataMember]
        public string ModuleName
        {
            get { return _moduleName; }
            set
            {
                if (_moduleName != value)
                {
                    _moduleName = value;
                    OnPropertyChanged("ModuleName");
                }
            }
        }
        private string _moduleName;
    
        [DataMember]
        public string ModuleIcon
        {
            get { return _moduleIcon; }
            set
            {
                if (_moduleIcon != value)
                {
                    _moduleIcon = value;
                    OnPropertyChanged("ModuleIcon");
                }
            }
        }
        private string _moduleIcon;
    
        [DataMember]
        public Nullable<long> ModulePermissionID
        {
            get { return _modulePermissionID; }
            set
            {
                if (_modulePermissionID != value)
                {
                    _modulePermissionID = value;
                    OnPropertyChanged("ModulePermissionID");
                }
            }
        }
        private Nullable<long> _modulePermissionID;

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
