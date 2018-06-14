namespace Loowoo.Land.OA.Service.Missive
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.OAMissiveProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.OAMissiveService = new System.ServiceProcess.ServiceInstaller();
            // 
            // OAMissiveProcessInstaller
            // 
            this.OAMissiveProcessInstaller.Account = System.ServiceProcess.ServiceAccount.NetworkService;
            this.OAMissiveProcessInstaller.Password = null;
            this.OAMissiveProcessInstaller.Username = null;
            // 
            // OAMissiveService
            // 
            this.OAMissiveService.DisplayName = "OA公文收发服务";
            this.OAMissiveService.ServiceName = "OAMissiveService";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.OAMissiveProcessInstaller,
            this.OAMissiveService});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller OAMissiveProcessInstaller;
        private System.ServiceProcess.ServiceInstaller OAMissiveService;
    }
}