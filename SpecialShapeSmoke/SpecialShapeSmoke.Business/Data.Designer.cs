﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

[assembly: EdmSchemaAttribute()]
namespace SpecialShapeSmoke.Business
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class DataEntities : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new DataEntities object using the connection string found in the 'DataEntities' section of the application configuration file.
        /// </summary>
        public DataEntities() : base("name=DataEntities", "DataEntities")
        {
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new DataEntities object.
        /// </summary>
        public DataEntities(string connectionString) : base(connectionString, "DataEntities")
        {
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new DataEntities object.
        /// </summary>
        public DataEntities(EntityConnection connection) : base(connection, "DataEntities")
        {
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<T_PRODUCE_REPLENISHPLAN> T_PRODUCE_REPLENISHPLAN
        {
            get
            {
                if ((_T_PRODUCE_REPLENISHPLAN == null))
                {
                    _T_PRODUCE_REPLENISHPLAN = base.CreateObjectSet<T_PRODUCE_REPLENISHPLAN>("T_PRODUCE_REPLENISHPLAN");
                }
                return _T_PRODUCE_REPLENISHPLAN;
            }
        }
        private ObjectSet<T_PRODUCE_REPLENISHPLAN> _T_PRODUCE_REPLENISHPLAN;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<HUNHEVIEW> HUNHEVIEW
        {
            get
            {
                if ((_HUNHEVIEW == null))
                {
                    _HUNHEVIEW = base.CreateObjectSet<HUNHEVIEW>("HUNHEVIEW");
                }
                return _HUNHEVIEW;
            }
        }
        private ObjectSet<HUNHEVIEW> _HUNHEVIEW;

        #endregion

        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the T_PRODUCE_REPLENISHPLAN EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToT_PRODUCE_REPLENISHPLAN(T_PRODUCE_REPLENISHPLAN t_PRODUCE_REPLENISHPLAN)
        {
            base.AddObject("T_PRODUCE_REPLENISHPLAN", t_PRODUCE_REPLENISHPLAN);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the HUNHEVIEW EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToHUNHEVIEW(HUNHEVIEW hUNHEVIEW)
        {
            base.AddObject("HUNHEVIEW", hUNHEVIEW);
        }

        #endregion

    }

    #endregion

    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="Model", Name="HUNHEVIEW")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class HUNHEVIEW : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new HUNHEVIEW object.
        /// </summary>
        /// <param name="pOKEID">Initial value of the POKEID property.</param>
        public static HUNHEVIEW CreateHUNHEVIEW(global::System.Decimal pOKEID)
        {
            HUNHEVIEW hUNHEVIEW = new HUNHEVIEW();
            hUNHEVIEW.POKEID = pOKEID;
            return hUNHEVIEW;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Decimal> TASKNUM
        {
            get
            {
                return _TASKNUM;
            }
            set
            {
                OnTASKNUMChanging(value);
                ReportPropertyChanging("TASKNUM");
                _TASKNUM = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("TASKNUM");
                OnTASKNUMChanged();
            }
        }
        private Nullable<global::System.Decimal> _TASKNUM;
        partial void OnTASKNUMChanging(Nullable<global::System.Decimal> value);
        partial void OnTASKNUMChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Decimal POKEID
        {
            get
            {
                return _POKEID;
            }
            set
            {
                if (_POKEID != value)
                {
                    OnPOKEIDChanging(value);
                    ReportPropertyChanging("POKEID");
                    _POKEID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("POKEID");
                    OnPOKEIDChanged();
                }
            }
        }
        private global::System.Decimal _POKEID;
        partial void OnPOKEIDChanging(global::System.Decimal value);
        partial void OnPOKEIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String CIGARETTECODE
        {
            get
            {
                return _CIGARETTECODE;
            }
            set
            {
                OnCIGARETTECODEChanging(value);
                ReportPropertyChanging("CIGARETTECODE");
                _CIGARETTECODE = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("CIGARETTECODE");
                OnCIGARETTECODEChanged();
            }
        }
        private global::System.String _CIGARETTECODE;
        partial void OnCIGARETTECODEChanging(global::System.String value);
        partial void OnCIGARETTECODEChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String CIGARETTENAME
        {
            get
            {
                return _CIGARETTENAME;
            }
            set
            {
                OnCIGARETTENAMEChanging(value);
                ReportPropertyChanging("CIGARETTENAME");
                _CIGARETTENAME = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("CIGARETTENAME");
                OnCIGARETTENAMEChanged();
            }
        }
        private global::System.String _CIGARETTENAME;
        partial void OnCIGARETTENAMEChanging(global::System.String value);
        partial void OnCIGARETTENAMEChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Decimal> QUANTITY
        {
            get
            {
                return _QUANTITY;
            }
            set
            {
                OnQUANTITYChanging(value);
                ReportPropertyChanging("QUANTITY");
                _QUANTITY = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("QUANTITY");
                OnQUANTITYChanged();
            }
        }
        private Nullable<global::System.Decimal> _QUANTITY;
        partial void OnQUANTITYChanging(Nullable<global::System.Decimal> value);
        partial void OnQUANTITYChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Decimal> MACHINESEQ
        {
            get
            {
                return _MACHINESEQ;
            }
            set
            {
                OnMACHINESEQChanging(value);
                ReportPropertyChanging("MACHINESEQ");
                _MACHINESEQ = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("MACHINESEQ");
                OnMACHINESEQChanged();
            }
        }
        private Nullable<global::System.Decimal> _MACHINESEQ;
        partial void OnMACHINESEQChanging(Nullable<global::System.Decimal> value);
        partial void OnMACHINESEQChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String CUSTOMERNAME
        {
            get
            {
                return _CUSTOMERNAME;
            }
            set
            {
                OnCUSTOMERNAMEChanging(value);
                ReportPropertyChanging("CUSTOMERNAME");
                _CUSTOMERNAME = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("CUSTOMERNAME");
                OnCUSTOMERNAMEChanged();
            }
        }
        private global::System.String _CUSTOMERNAME;
        partial void OnCUSTOMERNAMEChanging(global::System.String value);
        partial void OnCUSTOMERNAMEChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String REGIONCODE
        {
            get
            {
                return _REGIONCODE;
            }
            set
            {
                OnREGIONCODEChanging(value);
                ReportPropertyChanging("REGIONCODE");
                _REGIONCODE = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("REGIONCODE");
                OnREGIONCODEChanged();
            }
        }
        private global::System.String _REGIONCODE;
        partial void OnREGIONCODEChanging(global::System.String value);
        partial void OnREGIONCODEChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Decimal> STATUS
        {
            get
            {
                return _STATUS;
            }
            set
            {
                OnSTATUSChanging(value);
                ReportPropertyChanging("STATUS");
                _STATUS = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("STATUS");
                OnSTATUSChanged();
            }
        }
        private Nullable<global::System.Decimal> _STATUS;
        partial void OnSTATUSChanging(Nullable<global::System.Decimal> value);
        partial void OnSTATUSChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Decimal> SORTSEQ
        {
            get
            {
                return _SORTSEQ;
            }
            set
            {
                OnSORTSEQChanging(value);
                ReportPropertyChanging("SORTSEQ");
                _SORTSEQ = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("SORTSEQ");
                OnSORTSEQChanged();
            }
        }
        private Nullable<global::System.Decimal> _SORTSEQ;
        partial void OnSORTSEQChanging(Nullable<global::System.Decimal> value);
        partial void OnSORTSEQChanged();

        #endregion

    
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="Model", Name="T_PRODUCE_REPLENISHPLAN")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class T_PRODUCE_REPLENISHPLAN : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new T_PRODUCE_REPLENISHPLAN object.
        /// </summary>
        /// <param name="id">Initial value of the ID property.</param>
        public static T_PRODUCE_REPLENISHPLAN CreateT_PRODUCE_REPLENISHPLAN(global::System.Decimal id)
        {
            T_PRODUCE_REPLENISHPLAN t_PRODUCE_REPLENISHPLAN = new T_PRODUCE_REPLENISHPLAN();
            t_PRODUCE_REPLENISHPLAN.ID = id;
            return t_PRODUCE_REPLENISHPLAN;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Decimal ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (_ID != value)
                {
                    OnIDChanging(value);
                    ReportPropertyChanging("ID");
                    _ID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("ID");
                    OnIDChanged();
                }
            }
        }
        private global::System.Decimal _ID;
        partial void OnIDChanging(global::System.Decimal value);
        partial void OnIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String CIGARETTECODE
        {
            get
            {
                return _CIGARETTECODE;
            }
            set
            {
                OnCIGARETTECODEChanging(value);
                ReportPropertyChanging("CIGARETTECODE");
                _CIGARETTECODE = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("CIGARETTECODE");
                OnCIGARETTECODEChanged();
            }
        }
        private global::System.String _CIGARETTECODE;
        partial void OnCIGARETTECODEChanging(global::System.String value);
        partial void OnCIGARETTECODEChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String CIGARETTENAME
        {
            get
            {
                return _CIGARETTENAME;
            }
            set
            {
                OnCIGARETTENAMEChanging(value);
                ReportPropertyChanging("CIGARETTENAME");
                _CIGARETTENAME = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("CIGARETTENAME");
                OnCIGARETTENAMEChanged();
            }
        }
        private global::System.String _CIGARETTENAME;
        partial void OnCIGARETTENAMEChanging(global::System.String value);
        partial void OnCIGARETTENAMEChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Decimal> REPLENISHQTY
        {
            get
            {
                return _REPLENISHQTY;
            }
            set
            {
                OnREPLENISHQTYChanging(value);
                ReportPropertyChanging("REPLENISHQTY");
                _REPLENISHQTY = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("REPLENISHQTY");
                OnREPLENISHQTYChanged();
            }
        }
        private Nullable<global::System.Decimal> _REPLENISHQTY;
        partial void OnREPLENISHQTYChanging(Nullable<global::System.Decimal> value);
        partial void OnREPLENISHQTYChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Decimal> ISCOMPLETED
        {
            get
            {
                return _ISCOMPLETED;
            }
            set
            {
                OnISCOMPLETEDChanging(value);
                ReportPropertyChanging("ISCOMPLETED");
                _ISCOMPLETED = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("ISCOMPLETED");
                OnISCOMPLETEDChanged();
            }
        }
        private Nullable<global::System.Decimal> _ISCOMPLETED;
        partial void OnISCOMPLETEDChanging(Nullable<global::System.Decimal> value);
        partial void OnISCOMPLETEDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String TROUGHNUM
        {
            get
            {
                return _TROUGHNUM;
            }
            set
            {
                OnTROUGHNUMChanging(value);
                ReportPropertyChanging("TROUGHNUM");
                _TROUGHNUM = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("TROUGHNUM");
                OnTROUGHNUMChanged();
            }
        }
        private global::System.String _TROUGHNUM;
        partial void OnTROUGHNUMChanging(global::System.String value);
        partial void OnTROUGHNUMChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Decimal> REPLENISHLINE
        {
            get
            {
                return _REPLENISHLINE;
            }
            set
            {
                OnREPLENISHLINEChanging(value);
                ReportPropertyChanging("REPLENISHLINE");
                _REPLENISHLINE = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("REPLENISHLINE");
                OnREPLENISHLINEChanged();
            }
        }
        private Nullable<global::System.Decimal> _REPLENISHLINE;
        partial void OnREPLENISHLINEChanging(Nullable<global::System.Decimal> value);
        partial void OnREPLENISHLINEChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Decimal> STATUS
        {
            get
            {
                return _STATUS;
            }
            set
            {
                OnSTATUSChanging(value);
                ReportPropertyChanging("STATUS");
                _STATUS = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("STATUS");
                OnSTATUSChanged();
            }
        }
        private Nullable<global::System.Decimal> _STATUS;
        partial void OnSTATUSChanging(Nullable<global::System.Decimal> value);
        partial void OnSTATUSChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Decimal> TRANSPORTATIONLINE
        {
            get
            {
                return _TRANSPORTATIONLINE;
            }
            set
            {
                OnTRANSPORTATIONLINEChanging(value);
                ReportPropertyChanging("TRANSPORTATIONLINE");
                _TRANSPORTATIONLINE = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("TRANSPORTATIONLINE");
                OnTRANSPORTATIONLINEChanged();
            }
        }
        private Nullable<global::System.Decimal> _TRANSPORTATIONLINE;
        partial void OnTRANSPORTATIONLINEChanging(Nullable<global::System.Decimal> value);
        partial void OnTRANSPORTATIONLINEChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> FINISHTIME
        {
            get
            {
                return _FINISHTIME;
            }
            set
            {
                OnFINISHTIMEChanging(value);
                ReportPropertyChanging("FINISHTIME");
                _FINISHTIME = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("FINISHTIME");
                OnFINISHTIMEChanged();
            }
        }
        private Nullable<global::System.DateTime> _FINISHTIME;
        partial void OnFINISHTIMEChanging(Nullable<global::System.DateTime> value);
        partial void OnFINISHTIMEChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String JYCODE
        {
            get
            {
                return _JYCODE;
            }
            set
            {
                OnJYCODEChanging(value);
                ReportPropertyChanging("JYCODE");
                _JYCODE = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("JYCODE");
                OnJYCODEChanged();
            }
        }
        private global::System.String _JYCODE;
        partial void OnJYCODEChanging(global::System.String value);
        partial void OnJYCODEChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Decimal> TYPE
        {
            get
            {
                return _TYPE;
            }
            set
            {
                OnTYPEChanging(value);
                ReportPropertyChanging("TYPE");
                _TYPE = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("TYPE");
                OnTYPEChanged();
            }
        }
        private Nullable<global::System.Decimal> _TYPE;
        partial void OnTYPEChanging(Nullable<global::System.Decimal> value);
        partial void OnTYPEChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String TASKNUM
        {
            get
            {
                return _TASKNUM;
            }
            set
            {
                OnTASKNUMChanging(value);
                ReportPropertyChanging("TASKNUM");
                _TASKNUM = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("TASKNUM");
                OnTASKNUMChanged();
            }
        }
        private global::System.String _TASKNUM;
        partial void OnTASKNUMChanging(global::System.String value);
        partial void OnTASKNUMChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Decimal> MANTISSA
        {
            get
            {
                return _MANTISSA;
            }
            set
            {
                OnMANTISSAChanging(value);
                ReportPropertyChanging("MANTISSA");
                _MANTISSA = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("MANTISSA");
                OnMANTISSAChanged();
            }
        }
        private Nullable<global::System.Decimal> _MANTISSA;
        partial void OnMANTISSAChanging(Nullable<global::System.Decimal> value);
        partial void OnMANTISSAChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> SCANTIME
        {
            get
            {
                return _SCANTIME;
            }
            set
            {
                OnSCANTIMEChanging(value);
                ReportPropertyChanging("SCANTIME");
                _SCANTIME = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("SCANTIME");
                OnSCANTIMEChanged();
            }
        }
        private Nullable<global::System.DateTime> _SCANTIME;
        partial void OnSCANTIMEChanging(Nullable<global::System.DateTime> value);
        partial void OnSCANTIMEChanged();

        #endregion

    
    }

    #endregion

    
}
