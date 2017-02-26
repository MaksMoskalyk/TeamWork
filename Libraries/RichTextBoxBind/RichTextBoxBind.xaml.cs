using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfControls
{
    /// <summary>
    /// Interaction logic for RichTextBoxBind.xaml
    /// </summary>

    public partial class RichTextBoxBind : UserControl
    {
        #region Fields
        // Member variables
        private int m_InternalUpdatePending;
        private bool m_TextHasChanged;

        #endregion

        #region Dependency Property Declarations



        // Document property
        public static readonly DependencyProperty DocumentProperty =
            DependencyProperty.Register("Document", typeof(FlowDocument),
            typeof(RichTextBoxBind), new FrameworkPropertyMetadata
            (new PropertyChangedCallback(OnDocumentChanged))
            {
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = System.Windows.Data.UpdateSourceTrigger.PropertyChanged
            });

        // InsTxtCrtPstn property
        public static readonly DependencyProperty InsTxtCrtPstnProperty =
            DependencyProperty.Register("InsTxtCrtPstn", typeof(string),
            typeof(RichTextBoxBind), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnInsTxtCrtPstnChanged))
            {
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = System.Windows.Data.UpdateSourceTrigger.PropertyChanged
            });
        // insert text property
        public static readonly DependencyProperty bInsTextProperty =
    DependencyProperty.Register("bInsText", typeof(bool),
    typeof(RichTextBoxBind), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnInsTextPropertyChanged))
    {
        BindsTwoWayByDefault = true,
        DefaultUpdateSourceTrigger = System.Windows.Data.UpdateSourceTrigger.PropertyChanged
    });
        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public RichTextBoxBind()
        {
            InitializeComponent();
            bInsText = false;
            this.Initialize();
        }

        #endregion

        #region Properties
        /// <summary>
        /// The WPF FlowDocument contained in the control.
        /// </summary>
        public FlowDocument Document
        {
            get { return (FlowDocument)GetValue(DocumentProperty); }
            set { SetValue(DocumentProperty, value); }
        }
        public string InsTxtCrtPstn
        {
            get { return (string)GetValue(InsTxtCrtPstnProperty); }
            set { SetValue(InsTxtCrtPstnProperty, value); }
        }
        public bool bInsText
        {
            get { return (bool)GetValue(bInsTextProperty); }
            set { SetValue(bInsTextProperty, value); }
        }

        #endregion

        #region PropertyChanged Callback Methods

        /// <summary>
        /// Called when the Document property is changed
        /// </summary>
        private static void OnDocumentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Initialize
            var thisControl = (RichTextBoxBind)d;

            // Exit if this update was internally generated
            if (thisControl.m_InternalUpdatePending > 0)
            {

                // Decrement flags and exit
                thisControl.m_InternalUpdatePending--;
                return;
            }

            // Set Document property on RichTextBox
            thisControl.TextBox.Document = (e.NewValue == null) ? new FlowDocument() : (FlowDocument)e.NewValue;

            // Reset flag
            thisControl.m_TextHasChanged = false;
        }

        private static void OnInsTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {


            // Initialize
            var thisControl = (RichTextBoxBind)d;

            thisControl.bInsText = (bool)e.NewValue;

        }
        private static void OnInsTxtCrtPstnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {


            // Initialize
            var thisControl = (RichTextBoxBind)d;

            thisControl.InsTxtCrtPstn = (string)e.NewValue;

        }
        #endregion

        #region Event Handlers

        /// <summary>
        ///  Invoked when the user changes text in this user control.
        /// </summary>
        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            // Set the TextChanged flag
            m_TextHasChanged = true;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Forces an update of the Document property.
        /// </summary>
        public void UpdateDocumentBindings()
        {
            // Exit if text hasn't changed
            if (!m_TextHasChanged) return;

            // Set 'Internal Update Pending' flag
            m_InternalUpdatePending = 2;

            // Set Document property
            SetValue(DocumentProperty, this.TextBox.Document);
            if (InsTxtCrtPstn != null && bInsText)
            {
                TextBox.CaretPosition.InsertTextInRun(InsTxtCrtPstn);
                bInsText = false;
            }
        }
        public void setImage(string path)
        {
            BitmapImage bitmapSmiley = new BitmapImage(new Uri(path, UriKind.Relative));
            Image smiley = new Image();
            smiley.Source = bitmapSmiley;
            smiley.Width = 15;
            smiley.Height = 15;
            new InlineUIContainer(smiley, TextBox.CaretPosition);
            TextBox.CaretPosition = TextBox.CaretPosition.DocumentEnd;
        }
        public void ClearRTB()
        {
            TextBox.Document.Blocks.Clear();
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Initializes the control.
        /// </summary>
        private void Initialize()
        {

        }
        #endregion
    }
}
