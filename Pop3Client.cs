using System;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
using ComponentPro;
using ComponentPro.Net;
using ComponentPro.Net.Mail;
using ComponentPro.Security.Certificates;
using Pop3Samples.Security;

namespace Pop3Samples
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Defines connection states.
        /// </summary>
        enum ConnectionState
        {
            NotConnected,
            Connecting,
            Ready,
            Disconnecting
        }

        private readonly bool _exception; // Incidates whether a licensing exception has occurred. 

        private LoginInfo _settings; // Login information.
        private ConnectionState _state; // Current connection state.
        private int _lastColumnToSort;
        private SortOrder _lastSortOrder;

        public MainForm()
        {
            Application.EnableVisualStyles();

            try
            {
                InitializeComponent();
            }
            catch (Exception exc)
            {
                if (exc is ComponentPro.Licensing.Mail.UltimateLicenseException)
                {
                    MessageBox.Show(exc.Message, "Error");
                    _exception = true;
                    return;
                }

                throw;
            }

#if !Framework4_5
            this.client.DisconnectCompleted += this.client_DisconnectCompleted;
            this.client.ListMessagesCompleted += this.client_ListMessagesCompleted;
            this.client.AuthenticateCompleted += this.client_AuthenticateCompleted;
            this.client.CertificateReceived += this.client_CertificateReceived;
            this.client.CertificateRequired += this.client_CertificateRequired;
            this.client.ConnectCompleted += this.client_ConnectCompleted;
#endif
        }

        /// <summary>
        /// Handles the form's Load event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // Close the application if an exception occurred.
            if (_exception)
                this.Close();

            // Load settings from the Registry.
            _settings = LoginInfo.LoadConfig();
        }

        /// <summary>
        /// Handles the form's Closed event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected override void OnClosed(EventArgs e)
        {
            // Make sure to close the POP3 session before leaving.
            if (tsbLogout.Visible)
            {
                // Close the connection.
                Disconnect();

                // Wait for the completion.
                while (_state != ConnectionState.NotConnected)
                {
                    System.Threading.Thread.Sleep(50);
                    // Wake up the GUI.
                    System.Windows.Forms.Application.DoEvents();
                }
            }

            // And save the settings.
            _settings.SaveConfig();

            base.OnClosed(e);            
        }

        private void EnableDialog(bool enable)
        {
            listView.Enabled = enable;
            tsbLogin.Enabled = enable; tsbLogout.Enabled = enable;
            tsbRefresh.Enabled = enable && _state == ConnectionState.Ready; 
            
            Util.EnableCloseButton(this, enable);
        }

        /// <summary>
        /// Sets the status text.
        /// </summary>
        /// <param name="str">The new status text.</param>
        private void SetStatusText(string str)
        {
            toolStripStatusLabel.Text = str;
        }

        /// <summary>
        /// Sets the status text.
        /// </summary>
        /// <param name="str">The new status format text.</param>
        /// <param name="parameters">The parameters.</param>
        private void SetStatusText(string str, params object[] parameters)
        {
            toolStripStatusLabel.Text = string.Format(str, parameters);
        }

        /// <summary>
        /// Closes the connection and release used resources.
        /// </summary>
#if Framework4_5
        async
#endif
        void Disconnect()
        {
            // Disable the session timeout timer.
            sessionTimeoutTimer.Enabled = false;
            SetStatusText("Applying changes...");
            Application.DoEvents();
            foreach (ListViewItem i in listView.Items)
            {
                if (i.SubItems[6].Text == "Delete")
                {
                    client.Delete((int)i.Tag);
                }
            }

            SetStatusText("Disconnecting...");
            // Set Disconnecting state.
            _state = ConnectionState.Disconnecting;
            
#if Framework4_5
            try
            {
                // Asynchronously disconnect.
                await client.DisconnectAsync();
            }
            catch (Exception ex)
            {
                Util.ShowError(ex);
            }

            SetDisconnectedUIState();
#else
            // Asynchronously disconnect.
            client.DisconnectAsync();
#endif
        }

        /// <summary>
        /// Sets disconnected state.
        /// </summary>
        private void SetDisconnectedUIState()
        {
            SetStatusText("Ready");
            // Set state to not connected.
            _state = ConnectionState.NotConnected;
            // Enable and disable controls.
            EnableDialog(true);

            toolStripProgressBar.Visible = false;
            toolStripProgressCancelButton.Visible = false;
            toolStripProgressLabel.Visible = false;
            tsbLogin.Visible = true;
            tsbLogout.Visible = false;
            tsbRefresh.Enabled = false;
            listView.Items.Clear();
            tsbDelete.Enabled = false;
            tsbUndelete.Enabled = false;
            tsbOpen.Enabled = false;
            tsbSaveAs.Enabled = false;

            // Make sure all toolbar buttons' states are updated. 
            lvm_SelectedIndexChanged(null, null);
        }

#if !Framework4_5
        /// <summary>
        /// Handles the client's DisconnectCompleted event.
        /// </summary>
        /// <param name="sender">The client object.</param>
        /// <param name="e">The event arguments.</param>
        private void client_DisconnectCompleted(object sender, ExtendedAsyncCompletedEventArgs<string> e)
        {
            if (e.Error != null)
            {
                Util.ShowError(e.Error);
            }

            // Set disconnected state.
            SetDisconnectedUIState();
        }
#endif

        /// <summary>
        /// Logins to the POP3 server.
        /// </summary>
#if Framework4_5
        async void Connect()
#else
        void Connect()
#endif
        {
            // Set timeout.
            client.Timeout = _settings.Timeout;

            WebProxyEx proxy = new WebProxyEx();
            client.Proxy = proxy;
            // Setup proxy.
            if (_settings.ProxyServer.Length > 0 && _settings.ProxyPort > 0)
            {
                proxy.Server = _settings.ProxyServer;
                proxy.Port = _settings.ProxyPort;
                proxy.UserName = _settings.ProxyUserName;
                proxy.Password = _settings.ProxyPassword;
                proxy.Domain = _settings.ProxyDomain;
                proxy.ProxyType = _settings.ProxyType;
                proxy.AuthenticationMethod = _settings.ProxyAuthenticationMethod;
            }

            SetStatusText("Connecting to {0}:{1}...", _settings.Server, _settings.Port);
            _state = ConnectionState.Connecting;
            // Disable controls.
            EnableDialog(false);
                
#if Framework4_5
            try
            {
                // Connect to the POP3 server.
                await client.ConnectAsync(_settings.Server, _settings.Port, _settings.SecurityMode);
            }
            catch (Exception exc)
            {
                Util.ShowError(exc);
                Disconnect();
                return;
            }

            Authenticate();
#else
            // Connect to the POP3 server.
            client.ConnectAsync(_settings.Server, _settings.Port, _settings.SecurityMode);
#endif
        }

#if !Framework4_5
        /// <summary>
        /// Handles the client's ConnectCompleted event.
        /// </summary>
        /// <param name="sender">The client object.</param>
        /// <param name="e">The event arguments.</param>
        private void client_ConnectCompleted(object sender, ExtendedAsyncCompletedEventArgs<string> e)
        {
            if (e.Error != null)
            {
                Util.ShowError(e.Error);
                Disconnect();
                return;
            }

            Authenticate();
        }
#endif



#if Framework4_5
        async void Authenticate()
#else
        void Authenticate()
#endif
        {
            if (_state == ConnectionState.Disconnecting)
            {
                Disconnect();
                return;
            }
            SetStatusText("Logging in as {0}...", _settings.UserName);

#if Framework4_5
            try
            {
                // Asynchronously login to the POP3 server.
                await client.AuthenticateAsync(_settings.UserName, _settings.Password, _settings.Method);
            }
            catch (Exception exc)
            {
                Util.ShowError(exc);
                Disconnect();
                return;
            }

            // Refresh the message list.
            RefreshList();
#else
            // Asynchronously login to the POP3 server.
            client.AuthenticateAsync(_settings.UserName, _settings.Password, _settings.Method);
#endif
        }

#if !Framework4_5
        /// <summary>
        /// Handles the client's AuthenticateCompleted event.
        /// </summary>
        /// <param name="sender">The client object.</param>
        /// <param name="e">The event arguments.</param>
        private void client_AuthenticateCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Util.ShowError(e.Error);
                // Close the connection if an error occurred.
                Disconnect();
                return;
            }

            // Refresh the message list.
            RefreshList();
        }
#endif

        /// <summary>
        /// Refreshes the message list.
        /// </summary>
#if Framework4_5
        async void RefreshList()
#else
        void RefreshList()
#endif
        {
            // If disconnecting?
            if (_state == ConnectionState.Disconnecting)
            {
                Disconnect();
                return;
            }
            
            SetStatusText("Retrieving message list...");
            
            if ((_settings.DownloadOption & Pop3EnvelopeParts.FullHeaders) == Pop3EnvelopeParts.FullHeaders)
            {
                // Enable progress bar control.
                toolStripProgressBar.Visible = true;
                toolStripProgressCancelButton.Visible = true;
                toolStripProgressLabel.Visible = true; toolStripProgressLabel.Text = string.Empty;
                toolStripProgressBar.Value = 0;
            }
            
#if Framework4_5
            try
            {
                // Asynchronously get message list.
                ShowMessageList(await client.ListMessagesAsync(_settings.DownloadOption));
            }
            catch (Exception exc)
            {
                Util.ShowError(exc);
                Disconnect();
                return;
            }
#else
            // Asynchronously get message list.
            client.ListMessagesAsync(_settings.DownloadOption);
#endif
        }

        void ShowMessageList(Pop3MessageCollection list)
        {
            if (_state == ConnectionState.Disconnecting)
            {
                Disconnect();
                return;
            }

            // Clear the message list.
            listView.Items.Clear();

            if ((_settings.DownloadOption & Pop3EnvelopeParts.FullHeaders) == Pop3EnvelopeParts.FullHeaders)
                foreach (Pop3Message msg in list)
                {
                    // If From property is empty then use the Sender property.
                    string from = msg.From.ToString();
                    if (from.Length == 0 && msg.Sender != null)
                        from = msg.Sender.ToString();

                    string[] arr = new string[7] { msg.MessageInboxIndex.ToString(), msg.UniqueId, msg.Size.ToString(), from, msg.Subject, msg.Date != null ? msg.Date.ToString() : "1/1/1900", string.Empty };
                    ListViewItem lvi = new ListViewItem(arr);
                    lvi.Tag = msg.MessageInboxIndex;
                    listView.Items.Add(lvi);
                }
            else
                foreach (Pop3Message msg in list)
                {
                    string[] arr = new string[7] { msg.MessageInboxIndex.ToString(), msg.UniqueId, msg.Size.ToString(), string.Empty, string.Empty, string.Empty, string.Empty };
                    ListViewItem item = new ListViewItem(arr);
                    item.Tag = msg.MessageInboxIndex;
                    listView.Items.Add(item);
                }

            SetStatusText("Ready");
            _state = ConnectionState.Ready;
            EnableDialog(true);

            toolStripProgressBar.Visible = false;
            toolStripProgressCancelButton.Visible = false;
            toolStripProgressLabel.Visible = false;
            tsbLogin.Visible = false;
            tsbLogout.Visible = true;
            tsbRefresh.Enabled = true;

            lvm_SelectedIndexChanged(this, null);
        }

#if !Framework4_5
        /// <summary>
        /// Handles the client's ListMessagesCompleted event.
        /// </summary>
        /// <param name="sender">The client object.</param>
        /// <param name="e">The event arguments.</param>
        private void client_ListMessagesCompleted(object sender, ExtendedAsyncCompletedEventArgs<Pop3MessageCollection> e)
        {
            if (e.Error != null)
            {
                Util.ShowError(e.Error);
                Disconnect();
                return;
            }

            ShowMessageList(e.Result);
        }
#endif

        /// <summary>
        /// Handles the client's MessageListProgress event.
        /// </summary>
        /// <param name="sender">The client object.</param>
        /// <param name="e"></param>
        private void client_MessageListProgress(object sender, Pop3MessageListProgressEventArgs e)
        {
            toolStripProgressBar.Maximum = e.Total;
            toolStripProgressBar.Value = e.Downloaded;
            toolStripProgressLabel.Text = string.Format("{0}/{1} message(s) downloaded", e.Downloaded, e.Total);
        }

        /// <summary>
        /// Handles the client's Progress event.
        /// </summary>
        /// <param name="sender">The POP3 client object.</param>
        /// <param name="e">The event arguments.</param>
        void _client_Progress(object sender, Pop3ProgressEventArgs e)
        {
            toolStripProgressLabel.Text = string.Format("{0}/{1} downloaded", Util.FormatSize(e.BytesTransferred), Util.FormatSize(_messageSize));
            if (e.BytesTransferred < _messageSize)
                toolStripProgressBar.Value = (int)(e.BytesTransferred * 100 / _messageSize);
            Application.DoEvents();
        }

        #region List View

        private void lvm_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool enable = listView.SelectedItems.Count > 0;
            bool undoable = enable && (listView.SelectedItems.Count > 1 || listView.SelectedItems[0].SubItems[6].Text == "Delete");

            tsbDelete.Enabled = enable;
            tsbUndelete.Enabled = undoable;
            tsbOpen.Enabled = enable;
            tsbSaveAs.Enabled = enable;
        }

        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tsbOpen_Click(sender, null);
        }

        private void listView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (listView.Items.Count == 0)
                return;

            SortOrder sortOrder;
            if (_lastColumnToSort == e.Column)
            {
                if (_lastSortOrder == SortOrder.Ascending)
                    sortOrder = SortOrder.Descending;
                else
                    sortOrder = SortOrder.Ascending;
            }
            else
                sortOrder = SortOrder.Ascending;

            // Sort by name, size, or date time?
            switch (e.Column)
            {
                case 0:
                case 2:
                    listView.ListViewItemSorter = new ListViewItemSizeSorter(e.Column, sortOrder);
                    break;

                case 1:
                case 3:
                case 4:
                case 6:
                    listView.ListViewItemSorter = new ListViewItemNameSorter(e.Column, sortOrder);
                    break;

                case 5:
                    listView.ListViewItemSorter = new ListViewItemDateSorter(e.Column, sortOrder);
                    break;
            }

            _lastColumnToSort = e.Column;
            _lastSortOrder = sortOrder;
        }

        #endregion

        #region Menu Items

        private void toolStripProgressCancelButton_ButtonClick(object sender, EventArgs e)
        {
            client.Cancel();
        }

        private void openContextMenuItem_Click(object sender, EventArgs e)
        {
            tsbOpen_Click(sender, null);
        }

        private void deleteContextMenuItem_Click(object sender, EventArgs e)
        {
            tsbDelete_Click(sender, null);
        }

        private void undeleteContextMenuItem_Click(object sender, EventArgs e)
        {
            tsbUndelete_Click(sender, null);
        }

        private void saveMessageAsContextMenuItem_Click(object sender, EventArgs e)
        {
            tsbSaveAs_Click(sender, null);
        }

        private void sessionTimeoutTimer_Tick(object sender, EventArgs e)
        {
            if (!client.IsBusy && _state == ConnectionState.Ready && !client.IsConnected)
            {
                SetDisconnectedUIState();
            }
        }

        #endregion

        #region Toolbar

        private void tsbOpen_Click(object sender, EventArgs e)
        {
            _messageSize = long.Parse(listView.SelectedItems[0].SubItems[2].Text);
            MessageViewer form = new MessageViewer(client, (int)listView.SelectedItems[0].Tag, _messageSize);
            if (form.ShowDialog() == DialogResult.Abort)
            {
                // Disconnect and login again.
                client.Disconnect();
                Connect();
            }
        }

        long _messageSize;
        private void tsbSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog sav = new SaveFileDialog();
            sav.Filter = "Email files (*.eml)|*.eml|All files (*.*)|*.*";
            sav.FilterIndex = 1;
            sav.Title = "Save the mail as";

            // Show the Save File as dialog.
            if (sav.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    EnableDialog(false);

                    // Get sequence no.
                    int sequenceId = (int)listView.SelectedItems[0].Tag;
                    _messageSize = long.Parse(listView.SelectedItems[0].SubItems[2].Text);
                    client.Progress += _client_Progress;
                    toolStripProgressLabel.Visible = true;
                    toolStripProgressBar.Visible = true; toolStripProgressBar.Maximum = 100; toolStripProgressBar.Value = 0;
                    toolStripProgressCancelButton.Visible = true;
                    toolStripStatusLabel.Visible = false;
                    client.DownloadMessage(sequenceId, sav.FileName);
                }
                catch (Exception exc)
                {
                    Pop3Exception pexc = exc as Pop3Exception;
                    if (pexc == null || pexc.Status != MailClientExceptionStatus.OperationCancelled)
                        Util.ShowError(exc);
                    client.Disconnect();
                    Connect();
                }
                finally
                {
                    client.Progress -= _client_Progress;
                    toolStripProgressLabel.Visible = false;
                    toolStripProgressBar.Visible = false;
                    toolStripProgressCancelButton.Visible = false;
                    toolStripStatusLabel.Visible = true;
                    EnableDialog(true);
                }
            }
        }

        private void tsbLogin_Click(object sender, EventArgs e)
        {
            // Show the Login form.
            Login form = new Login(_settings);
            if (form.ShowDialog() == DialogResult.OK)
            {
                Connect();
            }
        }

        private void tsbLogout_Click(object sender, EventArgs e)
        {
            Disconnect();
        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            EnableDialog(false);
            RefreshList();
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem i in listView.SelectedItems)
            {
                i.SubItems[6].Text = "Delete";
            }

            lvm_SelectedIndexChanged(sender, null);
        }

        private void tsbUndelete_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem i in listView.SelectedItems)
            {
                i.SubItems[6].Text = string.Empty;
            }

            lvm_SelectedIndexChanged(sender, null);
        }

        #endregion

        #region Certificate

        /// <summary>
        /// Returns all issues of the given certificate.
        /// </summary>
        private static string GetCertProblem(CertificateVerificationStatus status, int code, ref bool showAddTrusted)
        {
            switch (status)
            {
                case CertificateVerificationStatus.TimeNotValid:
                    return "Server's certificate has expired or is not valid yet.";

                case CertificateVerificationStatus.Revoked:
                    return "Server's certificate has been revoked.";

                case CertificateVerificationStatus.UnknownCA:
                    return "Server's certificate was issued by an unknown authority.";

                case CertificateVerificationStatus.RootNotTrusted:
                    showAddTrusted = true;
                    return "Server's certificate was issued by an untrusted authority.";

                case CertificateVerificationStatus.IncompleteChain:
                    return "Server's certificate does not chain up to a trusted root authority.";

                case CertificateVerificationStatus.Malformed:
                    return "Server's certificate is malformed.";

                case CertificateVerificationStatus.CNNotMatch:
                    return "Server hostname does not match the certificate.";

                case CertificateVerificationStatus.UnknownError:
                    return string.Format("Error {0:x} encountered while validating server's certificate.", code);

                default:
                    return status.ToString();
            }
        }

        void client_CertificateReceived(object sender, ComponentPro.Security.CertificateReceivedEventArgs e)
        {
            CertValidator dlg = new CertValidator();

            CertificateVerificationStatus status = e.Status;

            CertificateVerificationStatus[] values = (CertificateVerificationStatus[])Enum.GetValues(typeof(CertificateVerificationStatus));

            StringBuilder sbIssues = new StringBuilder();
            bool showAddTrusted = false;
            for (int i = 0; i < values.Length; i++)
            {
                // Matches the validation status?
                if ((status & values[i]) == 0)
                    continue;

                // The issue is processed.
                status ^= values[i];

                sbIssues.AppendFormat("{0}\r\n", GetCertProblem(values[i], e.ErrorCode, ref showAddTrusted));
            }

            dlg.Certificate = e.ServerCertificates[0];
            dlg.Issues = sbIssues.ToString();
            dlg.ShowAddToTrustedList = showAddTrusted;

            dlg.ShowDialog();

            e.AddToTrustedRoot = dlg.AddToTrustedList;
            e.Accept = dlg.Accepted;
        }

        private void client_CertificateRequired(object sender, ComponentPro.Security.CertificateRequiredEventArgs e)
        {
            // If the client cert file is specified.
            if (!string.IsNullOrEmpty(_settings.Cert))
            {
                // Load Certificate.
                PasswordPrompt passdlg = new PasswordPrompt();
                // Ask for cert's passpharse
                if (passdlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    X509Certificate2 clientCert = new X509Certificate2(_settings.Cert, passdlg.Password);
                    e.Certificates = new X509Certificate2Collection(clientCert);
                    return;
                }

                // Password has not been provided.
            }
            CertProvider dlg = new CertProvider();
            dlg.ShowDialog();
            // Get the selected certificate.
            e.Certificates = new X509Certificate2Collection(dlg.SelectedCertificate);
        }

        #endregion
    }
}