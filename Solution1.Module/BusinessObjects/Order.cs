using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;

namespace Solution1.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Order : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Order(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        //private string _PersistentProperty;
        //[XafDisplayName("My display name"), ToolTip("My hint message")]
        //[ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)]
        //[Persistent("DatabaseColumnName"), RuleRequiredField(DefaultContexts.Save)]
        //public string PersistentProperty {
        //    get { return _PersistentProperty; }
        //    set { SetPropertyValue(nameof(PersistentProperty), ref _PersistentProperty, value); }
        //}

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.PersistentProperty = "Paid";
        //}

        private Customer _OrderCustomer;
        [Association]
        public Customer OrderCustomer
        {
            get { return _OrderCustomer; }
            set { SetPropertyValue<Customer>(nameof(OrderCustomer), ref _OrderCustomer, value); }
        }


        private DateTime _OrderDateTime;
        public DateTime OrderDateTime
        {
            get { return _OrderDateTime; }
            set { SetPropertyValue<DateTime>(nameof(OrderDateTime), ref _OrderDateTime, value); }
        }


        [Association]
        public XPCollection<Product> OrderProducts
        {
            get { return GetCollection<Product>(nameof(OrderProducts)); }
        }


        private int _OrderSum;
        public int OrderSum
        {
            get { return _OrderSum; }
            set
            {
                bool isEdit = SetPropertyValue<int>(nameof(OrderSum), ref _OrderSum, value);

                if(!IsLoading && !IsSaving && isEdit)
                {
                    XPCollection collection = new XPCollection(typeof(Product), CriteriaOperator.Parse("ProductOrders.Oid = Order.Oid"));

                    foreach(Product item in collection)
                        OrderSum += item.Price;
                }
            }
        }



    }
}