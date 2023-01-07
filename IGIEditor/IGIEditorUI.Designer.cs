namespace IGIEditor
{
    partial class IGIEditorUI
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IGIEditorUI));
            this.editorMainPanel = new System.Windows.Forms.Panel();
            this.quitLevelBtn = new System.Windows.Forms.Button();
            this.levelStartTxt = new System.Windows.Forms.NumericUpDown();
            this.restartLevelBtn = new System.Windows.Forms.Button();
            this.startGameBtn = new System.Windows.Forms.Button();
            this.xPosLbl = new System.Windows.Forms.Label();
            this.yPosLbl = new System.Windows.Forms.Label();
            this.zPosLbl = new System.Windows.Forms.Label();
            this.statusTxt = new System.Windows.Forms.Label();
            this.zPosLbl_M = new System.Windows.Forms.Label();
            this.yPosLbl_M = new System.Windows.Forms.Label();
            this.xPosLbl_M = new System.Windows.Forms.Label();
            this.editorTabs = new System.Windows.Forms.TabControl();
            this.levelEditor = new System.Windows.Forms.TabPage();
            this.objectImgBox = new System.Windows.Forms.PictureBox();
            this.clearAllLvlBtn = new System.Windows.Forms.Button();
            this.objectSelectDD = new System.Windows.Forms.ComboBox();
            this.buildingSelectDD = new System.Windows.Forms.ComboBox();
            this.removeObjectBtn = new System.Windows.Forms.Button();
            this.removeBuildingBtn = new System.Windows.Forms.Button();
            this.addObjectBtn = new System.Windows.Forms.Button();
            this.addBuildingBtn = new System.Windows.Forms.Button();
            this.objectEditor = new System.Windows.Forms.TabPage();
            this.modelIdOutLbl = new System.Windows.Forms.Label();
            this.modelNameTxt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buildingsResetTxt = new System.Windows.Forms.NumericUpDown();
            this.objectsResetTxt = new System.Windows.Forms.NumericUpDown();
            this.buildingsRemoveTxt = new System.Windows.Forms.NumericUpDown();
            this.objectsRemoveTxt = new System.Windows.Forms.NumericUpDown();
            this.resetBuildingsBtn = new System.Windows.Forms.Button();
            this.resetObjectsBtn = new System.Windows.Forms.Button();
            this.modelNameOutLbl = new System.Windows.Forms.Label();
            this.modelIDTxt = new System.Windows.Forms.TextBox();
            this.modelIdLbl = new System.Windows.Forms.Label();
            this.restoreModelBtn = new System.Windows.Forms.Button();
            this.removeModelBtn = new System.Windows.Forms.Button();
            this.removeBuildingsBtn = new System.Windows.Forms.Button();
            this.removeObjsBtn = new System.Windows.Forms.Button();
            this.humanEditor = new System.Windows.Forms.TabPage();
            this.inAirSpeedTxt = new System.Windows.Forms.NumericUpDown();
            this.upwardJumpTxt = new System.Windows.Forms.NumericUpDown();
            this.forwardJumpTxt = new System.Windows.Forms.NumericUpDown();
            this.peekTimeTxt = new System.Windows.Forms.NumericUpDown();
            this.peekCrouchTxt = new System.Windows.Forms.NumericUpDown();
            this.peekLRTxt = new System.Windows.Forms.NumericUpDown();
            this.humanViewCamTxt = new System.Windows.Forms.NumericUpDown();
            this.damageScaleFallTxt = new System.Windows.Forms.NumericUpDown();
            this.damageScaleFenceTxt = new System.Windows.Forms.NumericUpDown();
            this.teamIdTxt = new System.Windows.Forms.NumericUpDown();
            this.damageScaleTxt = new System.Windows.Forms.NumericUpDown();
            this.movementSpeedTxt = new System.Windows.Forms.NumericUpDown();
            this.updateHumanBtn = new System.Windows.Forms.Button();
            this.resetHumanBtn = new System.Windows.Forms.Button();
            this.inAirSpeedLbl = new System.Windows.Forms.Label();
            this.updatePeekBtn = new System.Windows.Forms.Button();
            this.updateHumanHealthBtn = new System.Windows.Forms.Button();
            this.damageScaleLbl = new System.Windows.Forms.Label();
            this.fallDamageLbl = new System.Windows.Forms.Label();
            this.fenceDamageScaleLbl = new System.Windows.Forms.Label();
            this.updateHumanSpeedBtn = new System.Windows.Forms.Button();
            this.peekTimeLbl = new System.Windows.Forms.Label();
            this.upwardJumpLbl = new System.Windows.Forms.Label();
            this.peekCrouchLbl = new System.Windows.Forms.Label();
            this.forwardJumpLbl = new System.Windows.Forms.Label();
            this.humanViewCamLbl = new System.Windows.Forms.Label();
            this.teamIdLbl = new System.Windows.Forms.Label();
            this.peekLRLbl = new System.Windows.Forms.Label();
            this.movementSpeedLbl = new System.Windows.Forms.Label();
            this.aiEditor = new System.Windows.Forms.TabPage();
            this.aiEditorTabs = new System.Windows.Forms.TabControl();
            this.aiEditorMainTab = new System.Windows.Forms.TabPage();
            this.teamIdText = new System.Windows.Forms.NumericUpDown();
            this.teamIdLabel = new System.Windows.Forms.Label();
            this.removeAllAiCb = new System.Windows.Forms.CheckBox();
            this.maxSpawnsTxt = new System.Windows.Forms.NumericUpDown();
            this.aiCountTxt = new System.Windows.Forms.NumericUpDown();
            this.removeAiBtn = new System.Windows.Forms.Button();
            this.graphAreaAiLbl = new System.Windows.Forms.TextBox();
            this.taskDetectionAiCb = new System.Windows.Forms.CheckBox();
            this.customAiCb = new System.Windows.Forms.CheckBox();
            this.maxSpawnsLbl = new System.Windows.Forms.Label();
            this.aiFriendlyCb = new System.Windows.Forms.CheckBox();
            this.aiAdvanceViewCb = new System.Windows.Forms.CheckBox();
            this.guardGeneratorCb = new System.Windows.Forms.CheckBox();
            this.aiInvincibleCb = new System.Windows.Forms.CheckBox();
            this.aiGraphIdDD = new System.Windows.Forms.ComboBox();
            this.aiTypeDD = new System.Windows.Forms.ComboBox();
            this.aiWeaponDD = new System.Windows.Forms.ComboBox();
            this.graphIdAiLbl = new System.Windows.Forms.Label();
            this.aiTypeLbl = new System.Windows.Forms.Label();
            this.weaponAiLbl = new System.Windows.Forms.Label();
            this.weaponAIImgBox = new System.Windows.Forms.PictureBox();
            this.aiModelSelectDD = new System.Windows.Forms.ComboBox();
            this.aiImgBox = new System.Windows.Forms.PictureBox();
            this.aiCountLbl = new System.Windows.Forms.Label();
            this.aiModelNameLbl = new System.Windows.Forms.Label();
            this.aiModelLbl = new System.Windows.Forms.Label();
            this.saveAIBtn = new System.Windows.Forms.Button();
            this.addAiBtn = new System.Windows.Forms.Button();
            this.aiJSONEditor = new System.Windows.Forms.TabPage();
            this.aiJsonAutoFmtCb = new System.Windows.Forms.CheckBox();
            this.aiJsonEditModeCb = new System.Windows.Forms.CheckBox();
            this.aiJsonClearDataCb = new System.Windows.Forms.CheckBox();
            this.aiJsonEditorTxt = new System.Windows.Forms.RichTextBox();
            this.aiJsonSaveBtn = new System.Windows.Forms.Button();
            this.aiJsonLoadBtn = new System.Windows.Forms.Button();
            this.aiJsonAddAiBtn = new System.Windows.Forms.Button();
            this.aiFileNameTxt = new System.Windows.Forms.TextBox();
            this.aiFileSizeTxt = new System.Windows.Forms.TextBox();
            this.aiJsonVersionTxt = new System.Windows.Forms.TextBox();
            this.aiJsonSaveAiBtn = new System.Windows.Forms.Button();
            this.aiScriptEditor = new System.Windows.Forms.TabPage();
            this.aiPatrolPathEditor = new System.Windows.Forms.TabPage();
            this.weaponEditor = new System.Windows.Forms.TabPage();
            this.weaponEditorTabs = new System.Windows.Forms.TabControl();
            this.weaponEditorMainTab = new System.Windows.Forms.TabPage();
            this.currentWeaponCb = new System.Windows.Forms.CheckBox();
            this.weaponGroupFileTxt = new System.Windows.Forms.TextBox();
            this.loadWeaponGroupBtn = new System.Windows.Forms.Button();
            this.saveWeaponGroupBtn = new System.Windows.Forms.Button();
            this.weaponAmmoTxt = new System.Windows.Forms.NumericUpDown();
            this.markWeaponsCb = new System.Windows.Forms.CheckBox();
            this.allWeaponsCb = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.weaponDD = new System.Windows.Forms.ComboBox();
            this.weaponImgBox = new System.Windows.Forms.PictureBox();
            this.removeWeaponBtn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.addWeaponBtn = new System.Windows.Forms.Button();
            this.weaponCfgEditor = new System.Windows.Forms.TabPage();
            this.resetWeaponBtn = new System.Windows.Forms.Button();
            this.weaponRangeTxt = new System.Windows.Forms.NumericUpDown();
            this.weaponRangeLbl = new System.Windows.Forms.Label();
            this.weaponRoundPerMinuteTxt = new System.Windows.Forms.NumericUpDown();
            this.weaponBulletsTxt = new System.Windows.Forms.NumericUpDown();
            this.weaponPowerTxt = new System.Windows.Forms.NumericUpDown();
            this.weaponDamageTxt = new System.Windows.Forms.NumericUpDown();
            this.weaponRoundPerClipTxt = new System.Windows.Forms.NumericUpDown();
            this.weaponRoundPerClipLbl = new System.Windows.Forms.Label();
            this.weaponRoundPerMinuteLbl = new System.Windows.Forms.Label();
            this.updateWeaponPowerDamageBtn = new System.Windows.Forms.Button();
            this.weaponBulletsLbl = new System.Windows.Forms.Label();
            this.weaponPowerLbl = new System.Windows.Forms.Label();
            this.weaponDamageLbl = new System.Windows.Forms.Label();
            this.weaponSfx2DD = new System.Windows.Forms.ComboBox();
            this.weaponSfx1DD = new System.Windows.Forms.ComboBox();
            this.weaponDisplayTypeDD = new System.Windows.Forms.ComboBox();
            this.sfx2Lbl = new System.Windows.Forms.Label();
            this.weaponSightTypeDD = new System.Windows.Forms.ComboBox();
            this.sfx1Lbl = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.updateWeaponSFXBtn = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.updateWeaponUIBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.weaponDescriptionTxt = new System.Windows.Forms.TextBox();
            this.weaponNameLbl = new System.Windows.Forms.Label();
            this.weaponCfgDD = new System.Windows.Forms.ComboBox();
            this.weaponTypeLblDD = new System.Windows.Forms.Label();
            this.updateWeaponDetailsBtn = new System.Windows.Forms.Button();
            this.weaponNameTxt = new System.Windows.Forms.TextBox();
            this.updateWeaponPropertiesBtn = new System.Windows.Forms.Button();
            this.weaponCfgAdvanceEditor = new System.Windows.Forms.TabPage();
            this.threeDEditor = new System.Windows.Forms.TabPage();
            this.resume3DEditorBtn = new System.Windows.Forms.Button();
            this.start3DEditorBtn = new System.Windows.Forms.Button();
            this.missionEditor = new System.Windows.Forms.TabPage();
            this.missionLevelLbl = new System.Windows.Forms.Label();
            this.missionAuthorLbl = new System.Windows.Forms.Label();
            this.missionSizeLbl = new System.Windows.Forms.Label();
            this.missionsOnlineDD = new System.Windows.Forms.ComboBox();
            this.uploadMissionBtn = new System.Windows.Forms.Button();
            this.missionServerList = new System.Windows.Forms.Label();
            this.missionLevelFlowTimerCb = new System.Windows.Forms.CheckBox();
            this.missionPlayTimeLbl = new System.Windows.Forms.Label();
            this.addLevelFlowBtn = new System.Windows.Forms.Button();
            this.downloadMissionBtn = new System.Windows.Forms.Button();
            this.installMissionBtn = new System.Windows.Forms.Button();
            this.missionDescTxt = new System.Windows.Forms.TextBox();
            this.missionDescLbl = new System.Windows.Forms.Label();
            this.missionRefreshBtn = new System.Windows.Forms.Button();
            this.removeMissionBtn = new System.Windows.Forms.Button();
            this.missionPlayTimeTxt = new System.Windows.Forms.TextBox();
            this.missionNameTxt = new System.Windows.Forms.TextBox();
            this.missionNameLbl = new System.Windows.Forms.Label();
            this.loadMissionBtn = new System.Windows.Forms.Button();
            this.saveMissionBtn = new System.Windows.Forms.Button();
            this.graphEditor = new System.Windows.Forms.TabPage();
            this.nodesInfoCb = new System.Windows.Forms.CheckBox();
            this.showAllGraphsCb = new System.Windows.Forms.CheckBox();
            this.gameItemsLbl = new System.Windows.Forms.Label();
            this.stopTraversingNodesBtn = new System.Windows.Forms.Button();
            this.manualTeleportGraphCb = new System.Windows.Forms.CheckBox();
            this.manualTeleportNodeCb = new System.Windows.Forms.CheckBox();
            this.autoTeleportGraphCb = new System.Windows.Forms.CheckBox();
            this.teleportToGraphBtn = new System.Windows.Forms.Button();
            this.autoTeleportNodeCb = new System.Windows.Forms.CheckBox();
            this.viewPortCameraEnableCb = new System.Windows.Forms.CheckBox();
            this.teleportToNodeBtn = new System.Windows.Forms.Button();
            this.nodeIdMetreCb = new System.Windows.Forms.CheckBox();
            this.nodeIdOffsetCb = new System.Windows.Forms.CheckBox();
            this.graphTotalNodesTxt = new System.Windows.Forms.TextBox();
            this.graphTotalNodesLbl = new System.Windows.Forms.Label();
            this.nodeCriteriaTxt = new System.Windows.Forms.TextBox();
            this.nodeZTxt = new System.Windows.Forms.TextBox();
            this.nodeZLbl = new System.Windows.Forms.Label();
            this.nodeYTxt = new System.Windows.Forms.TextBox();
            this.nodeYLbl = new System.Windows.Forms.Label();
            this.nodeXTxt = new System.Windows.Forms.TextBox();
            this.nodeXLbl = new System.Windows.Forms.Label();
            this.removeNodesBtn = new System.Windows.Forms.Button();
            this.removeLinksBtn = new System.Windows.Forms.Button();
            this.refreshLinksBtn = new System.Windows.Forms.Button();
            this.addLinksBtn = new System.Windows.Forms.Button();
            this.refreshNodesBtn = new System.Windows.Forms.Button();
            this.addNodesBtn = new System.Windows.Forms.Button();
            this.graphsMarkCb = new System.Windows.Forms.CheckBox();
            this.graphsAllCb = new System.Windows.Forms.CheckBox();
            this.nodesHilightCb = new System.Windows.Forms.CheckBox();
            this.nodesObjectsCb = new System.Windows.Forms.CheckBox();
            this.nodeIdDD = new System.Windows.Forms.ComboBox();
            this.nodeIdLbl = new System.Windows.Forms.Label();
            this.graphAreaLbl = new System.Windows.Forms.TextBox();
            this.graphIdDD = new System.Windows.Forms.ComboBox();
            this.graphIdLbl = new System.Windows.Forms.Label();
            this.positionEditor = new System.Windows.Forms.TabPage();
            this.resetPosCb = new System.Windows.Forms.CheckBox();
            this.gammaTxt = new System.Windows.Forms.NumericUpDown();
            this.betaTxt = new System.Windows.Forms.NumericUpDown();
            this.alphaTxt = new System.Windows.Forms.NumericUpDown();
            this.gammaLbl = new System.Windows.Forms.Label();
            this.betaLbl = new System.Windows.Forms.Label();
            this.alphaLbl = new System.Windows.Forms.Label();
            this.updateObjOrientation = new System.Windows.Forms.Button();
            this.humanPosMeterCb = new System.Windows.Forms.CheckBox();
            this.humanPosOffCb = new System.Windows.Forms.CheckBox();
            this.zPosTxt_H = new System.Windows.Forms.TextBox();
            this.zPosLbl_H = new System.Windows.Forms.Label();
            this.yPosTxt_H = new System.Windows.Forms.TextBox();
            this.yPosLbl_H = new System.Windows.Forms.Label();
            this.xPosTxt_H = new System.Windows.Forms.TextBox();
            this.xPosLbl_H = new System.Windows.Forms.Label();
            this.updateHumaPosition = new System.Windows.Forms.Button();
            this.objectPosDD = new System.Windows.Forms.ComboBox();
            this.objPosLbl = new System.Windows.Forms.Label();
            this.buildingPosDD = new System.Windows.Forms.ComboBox();
            this.buildingPosLbl = new System.Windows.Forms.Label();
            this.posCurrentCb = new System.Windows.Forms.CheckBox();
            this.posMeterCb = new System.Windows.Forms.CheckBox();
            this.posOffCb = new System.Windows.Forms.CheckBox();
            this.zPosObjTxt = new System.Windows.Forms.TextBox();
            this.zPosLbl_O = new System.Windows.Forms.Label();
            this.yPosObjTxt = new System.Windows.Forms.TextBox();
            this.yPosLbl_O = new System.Windows.Forms.Label();
            this.xPosObjTxt = new System.Windows.Forms.TextBox();
            this.xPosLbl_O = new System.Windows.Forms.Label();
            this.updateObjPosition = new System.Windows.Forms.Button();
            this.miscEditor = new System.Windows.Forms.TabPage();
            this.viewLogsCb = new System.Windows.Forms.CheckBox();
            this.shareLogsCb = new System.Windows.Forms.CheckBox();
            this.externalCompilerCb = new System.Windows.Forms.CheckBox();
            this.internalCompilerCb = new System.Windows.Forms.CheckBox();
            this.framesTxt = new System.Windows.Forms.NumericUpDown();
            this.minutesLbl = new System.Windows.Forms.Label();
            this.showAppDataCachePathBtn = new System.Windows.Forms.Button();
            this.gamePathBtn = new System.Windows.Forms.Button();
            this.editorUpdaterBtn = new System.Windows.Forms.Button();
            this.gameSettingsLbl = new System.Windows.Forms.Label();
            this.appSettingLbl = new System.Windows.Forms.Label();
            this.configLoadBtn = new System.Windows.Forms.Button();
            this.configSaveBtn = new System.Windows.Forms.Button();
            this.updateIntervalTxtx = new System.Windows.Forms.NumericUpDown();
            this.musicTrackBar = new System.Windows.Forms.TrackBar();
            this.musicSoundCb = new System.Windows.Forms.CheckBox();
            this.sfxMusicCb = new System.Windows.Forms.CheckBox();
            this.enableMusicCb = new System.Windows.Forms.CheckBox();
            this.debugModeCb = new System.Windows.Forms.CheckBox();
            this.udpateMusicBtn = new System.Windows.Forms.Button();
            this.gfxResetBtn = new System.Windows.Forms.Button();
            this.setFramesBtn = new System.Windows.Forms.Button();
            this.aiIdleCb = new System.Windows.Forms.CheckBox();
            this.appSupportBtn = new System.Windows.Forms.Button();
            this.disableWarningsCb = new System.Windows.Forms.CheckBox();
            this.showAppLogBtn = new System.Windows.Forms.Button();
            this.xmlCb = new System.Windows.Forms.CheckBox();
            this.jsonCb = new System.Windows.Forms.CheckBox();
            this.csvCb = new System.Windows.Forms.CheckBox();
            this.updateCheckerCb = new System.Windows.Forms.CheckBox();
            this.appLogsCb = new System.Windows.Forms.CheckBox();
            this.autoRefreshGameCb = new System.Windows.Forms.CheckBox();
            this.autoResetCb = new System.Windows.Forms.CheckBox();
            this.cutsceneRemoveBtn = new System.Windows.Forms.Button();
            this.resetScriptsFileBtn = new System.Windows.Forms.Button();
            this.resetLevelBtn = new System.Windows.Forms.Button();
            this.clearCacheBtn = new System.Windows.Forms.Button();
            this.exportObjectsBtn = new System.Windows.Forms.Button();
            this.devMode = new System.Windows.Forms.TabPage();
            this.devAutoFormatCb = new System.Windows.Forms.CheckBox();
            this.devClearContentsCb = new System.Windows.Forms.CheckBox();
            this.devViewerTxt = new System.Windows.Forms.RichTextBox();
            this.saveDevFileBtn = new System.Windows.Forms.Button();
            this.loadDevFileBtn = new System.Windows.Forms.Button();
            this.uploadUpdateBtn = new System.Windows.Forms.Button();
            this.devFileNameTxt = new System.Windows.Forms.TextBox();
            this.devFileSizeTxt = new System.Windows.Forms.TextBox();
            this.devVersionTxt = new System.Windows.Forms.TextBox();
            this.createUpdateBtn = new System.Windows.Forms.Button();
            this.levelNameLbl = new System.Windows.Forms.Label();
            this.levelImgBox = new System.Windows.Forms.PictureBox();
            this.appMenuSettings = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editorOnlineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startGameBtnMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.levelStartTxtMenu = new System.Windows.Forms.ToolStripTextBox();
            this.restartLevelBtnMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.quitLevelBtnMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshGameBtnMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.selectEditorModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playModeCb = new System.Windows.Forms.ToolStripMenuItem();
            this.editorModeCb = new System.Windows.Forms.ToolStripMenuItem();
            this.liveEditorCb = new System.Windows.Forms.ToolStripMenuItem();
            this.editorConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editorOnlineCb = new System.Windows.Forms.ToolStripMenuItem();
            this.startGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startWindowedGameBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.startFullScreenGameBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.gameStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.internalsStatusMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.internalsStatusLbl = new System.Windows.Forms.ToolStripMenuItem();
            this.profileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameProfileNameLbl = new System.Windows.Forms.ToolStripTextBox();
            this.gameProfileMissionLbl = new System.Windows.Forms.ToolStripMenuItem();
            this.compilerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compilerTypeLbl = new System.Windows.Forms.ToolStripTextBox();
            this.positionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.posCoordCb = new System.Windows.Forms.ToolStripMenuItem();
            this.posMetersCb = new System.Windows.Forms.ToolStripMenuItem();
            this.appSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applicationSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.versionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.versionLbl = new System.Windows.Forms.ToolStripTextBox();
            this.infoViewer = new System.Windows.Forms.ToolTip(this.components);
            this.editorMainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.levelStartTxt)).BeginInit();
            this.editorTabs.SuspendLayout();
            this.levelEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectImgBox)).BeginInit();
            this.objectEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buildingsResetTxt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectsResetTxt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buildingsRemoveTxt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectsRemoveTxt)).BeginInit();
            this.humanEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inAirSpeedTxt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upwardJumpTxt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.forwardJumpTxt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.peekTimeTxt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.peekCrouchTxt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.peekLRTxt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.humanViewCamTxt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.damageScaleFallTxt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.damageScaleFenceTxt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teamIdTxt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.damageScaleTxt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.movementSpeedTxt)).BeginInit();
            this.aiEditor.SuspendLayout();
            this.aiEditorTabs.SuspendLayout();
            this.aiEditorMainTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teamIdText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxSpawnsTxt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aiCountTxt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.weaponAIImgBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aiImgBox)).BeginInit();
            this.aiJSONEditor.SuspendLayout();
            this.weaponEditor.SuspendLayout();
            this.weaponEditorTabs.SuspendLayout();
            this.weaponEditorMainTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.weaponAmmoTxt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.weaponImgBox)).BeginInit();
            this.weaponCfgEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.weaponRangeTxt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.weaponRoundPerMinuteTxt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.weaponBulletsTxt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.weaponPowerTxt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.weaponDamageTxt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.weaponRoundPerClipTxt)).BeginInit();
            this.threeDEditor.SuspendLayout();
            this.missionEditor.SuspendLayout();
            this.graphEditor.SuspendLayout();
            this.positionEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gammaTxt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.betaTxt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.alphaTxt)).BeginInit();
            this.miscEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.framesTxt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.updateIntervalTxtx)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.musicTrackBar)).BeginInit();
            this.devMode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.levelImgBox)).BeginInit();
            this.appMenuSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // editorMainPanel
            // 
            this.editorMainPanel.BackColor = System.Drawing.SystemColors.Control;
            this.editorMainPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editorMainPanel.Controls.Add(this.quitLevelBtn);
            this.editorMainPanel.Controls.Add(this.levelStartTxt);
            this.editorMainPanel.Controls.Add(this.restartLevelBtn);
            this.editorMainPanel.Controls.Add(this.startGameBtn);
            this.editorMainPanel.Controls.Add(this.xPosLbl);
            this.editorMainPanel.Controls.Add(this.yPosLbl);
            this.editorMainPanel.Controls.Add(this.zPosLbl);
            this.editorMainPanel.Controls.Add(this.statusTxt);
            this.editorMainPanel.Controls.Add(this.zPosLbl_M);
            this.editorMainPanel.Controls.Add(this.yPosLbl_M);
            this.editorMainPanel.Controls.Add(this.xPosLbl_M);
            this.editorMainPanel.Controls.Add(this.editorTabs);
            this.editorMainPanel.Controls.Add(this.levelNameLbl);
            this.editorMainPanel.Controls.Add(this.levelImgBox);
            this.editorMainPanel.Controls.Add(this.appMenuSettings);
            this.editorMainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editorMainPanel.Location = new System.Drawing.Point(0, 0);
            this.editorMainPanel.Margin = new System.Windows.Forms.Padding(4);
            this.editorMainPanel.Name = "editorMainPanel";
            this.editorMainPanel.Size = new System.Drawing.Size(1147, 741);
            this.editorMainPanel.TabIndex = 0;
            // 
            // quitLevelBtn
            // 
            this.quitLevelBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.quitLevelBtn.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.quitLevelBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.quitLevelBtn.Location = new System.Drawing.Point(576, 172);
            this.quitLevelBtn.Margin = new System.Windows.Forms.Padding(4);
            this.quitLevelBtn.Name = "quitLevelBtn";
            this.quitLevelBtn.Size = new System.Drawing.Size(164, 33);
            this.quitLevelBtn.TabIndex = 45;
            this.quitLevelBtn.Text = "Quit";
            this.quitLevelBtn.UseVisualStyleBackColor = true;
            this.quitLevelBtn.Click += new System.EventHandler(this.quitLevelBtn_Click);
            // 
            // levelStartTxt
            // 
            this.levelStartTxt.BackColor = System.Drawing.SystemColors.Control;
            this.levelStartTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.levelStartTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.levelStartTxt.Location = new System.Drawing.Point(703, 71);
            this.levelStartTxt.Maximum = new decimal(new int[] {
            14,
            0,
            0,
            0});
            this.levelStartTxt.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.levelStartTxt.Name = "levelStartTxt";
            this.levelStartTxt.Size = new System.Drawing.Size(37, 30);
            this.levelStartTxt.TabIndex = 42;
            this.levelStartTxt.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.levelStartTxt.ValueChanged += new System.EventHandler(this.levelStartTxt_ValueChanged);
            // 
            // restartLevelBtn
            // 
            this.restartLevelBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.restartLevelBtn.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.restartLevelBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.restartLevelBtn.Location = new System.Drawing.Point(575, 124);
            this.restartLevelBtn.Margin = new System.Windows.Forms.Padding(4);
            this.restartLevelBtn.Name = "restartLevelBtn";
            this.restartLevelBtn.Size = new System.Drawing.Size(165, 33);
            this.restartLevelBtn.TabIndex = 26;
            this.restartLevelBtn.Text = "Restart";
            this.restartLevelBtn.UseVisualStyleBackColor = true;
            this.restartLevelBtn.Click += new System.EventHandler(this.restartLevelBtn_Click);
            // 
            // startGameBtn
            // 
            this.startGameBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startGameBtn.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.startGameBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.startGameBtn.Location = new System.Drawing.Point(575, 71);
            this.startGameBtn.Margin = new System.Windows.Forms.Padding(4);
            this.startGameBtn.Name = "startGameBtn";
            this.startGameBtn.Size = new System.Drawing.Size(121, 33);
            this.startGameBtn.TabIndex = 15;
            this.startGameBtn.Text = "Start";
            this.startGameBtn.UseVisualStyleBackColor = true;
            this.startGameBtn.Click += new System.EventHandler(this.startGameBtn_Click);
            // 
            // xPosLbl
            // 
            this.xPosLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.xPosLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.xPosLbl.Location = new System.Drawing.Point(281, 71);
            this.xPosLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.xPosLbl.Name = "xPosLbl";
            this.xPosLbl.Size = new System.Drawing.Size(245, 33);
            this.xPosLbl.TabIndex = 14;
            this.xPosLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.infoViewer.SetToolTip(this.xPosLbl, "Click to Copy Position");
            this.xPosLbl.Click += new System.EventHandler(this.xPosLbl_Click);
            // 
            // yPosLbl
            // 
            this.yPosLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.yPosLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.yPosLbl.Location = new System.Drawing.Point(281, 124);
            this.yPosLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.yPosLbl.Name = "yPosLbl";
            this.yPosLbl.Size = new System.Drawing.Size(245, 33);
            this.yPosLbl.TabIndex = 13;
            this.yPosLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.infoViewer.SetToolTip(this.yPosLbl, "Click to Copy Position");
            this.yPosLbl.Click += new System.EventHandler(this.yPosLbl_Click);
            // 
            // zPosLbl
            // 
            this.zPosLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.zPosLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.zPosLbl.Location = new System.Drawing.Point(281, 172);
            this.zPosLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.zPosLbl.Name = "zPosLbl";
            this.zPosLbl.Size = new System.Drawing.Size(245, 33);
            this.zPosLbl.TabIndex = 12;
            this.zPosLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.infoViewer.SetToolTip(this.zPosLbl, "Click to Copy Position");
            this.zPosLbl.Click += new System.EventHandler(this.zPosLbl_Click);
            // 
            // statusTxt
            // 
            this.statusTxt.AutoSize = true;
            this.statusTxt.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusTxt.ForeColor = System.Drawing.Color.Gray;
            this.statusTxt.Location = new System.Drawing.Point(327, 227);
            this.statusTxt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.statusTxt.Name = "statusTxt";
            this.statusTxt.Size = new System.Drawing.Size(100, 23);
            this.statusTxt.TabIndex = 11;
            this.statusTxt.Text = "Staus Text";
            this.statusTxt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // zPosLbl_M
            // 
            this.zPosLbl_M.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.zPosLbl_M.ForeColor = System.Drawing.SystemColors.ControlText;
            this.zPosLbl_M.Location = new System.Drawing.Point(236, 172);
            this.zPosLbl_M.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.zPosLbl_M.Name = "zPosLbl_M";
            this.zPosLbl_M.Size = new System.Drawing.Size(37, 33);
            this.zPosLbl_M.TabIndex = 9;
            this.zPosLbl_M.Text = "Z ";
            this.zPosLbl_M.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // yPosLbl_M
            // 
            this.yPosLbl_M.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.yPosLbl_M.ForeColor = System.Drawing.SystemColors.ControlText;
            this.yPosLbl_M.Location = new System.Drawing.Point(236, 124);
            this.yPosLbl_M.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.yPosLbl_M.Name = "yPosLbl_M";
            this.yPosLbl_M.Size = new System.Drawing.Size(37, 33);
            this.yPosLbl_M.TabIndex = 8;
            this.yPosLbl_M.Text = "Y ";
            this.yPosLbl_M.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // xPosLbl_M
            // 
            this.xPosLbl_M.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.xPosLbl_M.ForeColor = System.Drawing.SystemColors.ControlText;
            this.xPosLbl_M.Location = new System.Drawing.Point(236, 71);
            this.xPosLbl_M.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.xPosLbl_M.Name = "xPosLbl_M";
            this.xPosLbl_M.Size = new System.Drawing.Size(37, 33);
            this.xPosLbl_M.TabIndex = 7;
            this.xPosLbl_M.Text = "X ";
            this.xPosLbl_M.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // editorTabs
            // 
            this.editorTabs.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.editorTabs.Controls.Add(this.levelEditor);
            this.editorTabs.Controls.Add(this.objectEditor);
            this.editorTabs.Controls.Add(this.humanEditor);
            this.editorTabs.Controls.Add(this.aiEditor);
            this.editorTabs.Controls.Add(this.weaponEditor);
            this.editorTabs.Controls.Add(this.threeDEditor);
            this.editorTabs.Controls.Add(this.missionEditor);
            this.editorTabs.Controls.Add(this.graphEditor);
            this.editorTabs.Controls.Add(this.positionEditor);
            this.editorTabs.Controls.Add(this.miscEditor);
            this.editorTabs.Controls.Add(this.devMode);
            this.editorTabs.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editorTabs.HotTrack = true;
            this.editorTabs.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.editorTabs.Location = new System.Drawing.Point(0, 319);
            this.editorTabs.Margin = new System.Windows.Forms.Padding(4);
            this.editorTabs.Multiline = true;
            this.editorTabs.Name = "editorTabs";
            this.editorTabs.SelectedIndex = 0;
            this.editorTabs.ShowToolTips = true;
            this.editorTabs.Size = new System.Drawing.Size(1145, 421);
            this.editorTabs.TabIndex = 6;
            this.infoViewer.SetToolTip(this.editorTabs, "\r\n");
            this.editorTabs.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabContainer_Selected);
            // 
            // levelEditor
            // 
            this.levelEditor.BackColor = System.Drawing.SystemColors.Control;
            this.levelEditor.Controls.Add(this.objectImgBox);
            this.levelEditor.Controls.Add(this.clearAllLvlBtn);
            this.levelEditor.Controls.Add(this.objectSelectDD);
            this.levelEditor.Controls.Add(this.buildingSelectDD);
            this.levelEditor.Controls.Add(this.removeObjectBtn);
            this.levelEditor.Controls.Add(this.removeBuildingBtn);
            this.levelEditor.Controls.Add(this.addObjectBtn);
            this.levelEditor.Controls.Add(this.addBuildingBtn);
            this.levelEditor.ForeColor = System.Drawing.SystemColors.ControlText;
            this.levelEditor.Location = new System.Drawing.Point(4, 30);
            this.levelEditor.Margin = new System.Windows.Forms.Padding(4);
            this.levelEditor.Name = "levelEditor";
            this.levelEditor.Padding = new System.Windows.Forms.Padding(4);
            this.levelEditor.Size = new System.Drawing.Size(1137, 387);
            this.levelEditor.TabIndex = 1;
            this.levelEditor.Text = "Level Editor";
            // 
            // objectImgBox
            // 
            this.objectImgBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.objectImgBox.Location = new System.Drawing.Point(36, 46);
            this.objectImgBox.Margin = new System.Windows.Forms.Padding(4);
            this.objectImgBox.Name = "objectImgBox";
            this.objectImgBox.Size = new System.Drawing.Size(199, 311);
            this.objectImgBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.objectImgBox.TabIndex = 44;
            this.objectImgBox.TabStop = false;
            // 
            // clearAllLvlBtn
            // 
            this.clearAllLvlBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clearAllLvlBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.clearAllLvlBtn.Location = new System.Drawing.Point(919, 173);
            this.clearAllLvlBtn.Margin = new System.Windows.Forms.Padding(4);
            this.clearAllLvlBtn.Name = "clearAllLvlBtn";
            this.clearAllLvlBtn.Size = new System.Drawing.Size(211, 31);
            this.clearAllLvlBtn.TabIndex = 43;
            this.clearAllLvlBtn.Text = "Clear level";
            this.clearAllLvlBtn.UseVisualStyleBackColor = true;
            this.clearAllLvlBtn.Click += new System.EventHandler(this.clearAllLevelBtn_Click);
            // 
            // objectSelectDD
            // 
            this.objectSelectDD.BackColor = System.Drawing.SystemColors.Control;
            this.objectSelectDD.Cursor = System.Windows.Forms.Cursors.Hand;
            this.objectSelectDD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.objectSelectDD.ForeColor = System.Drawing.SystemColors.WindowText;
            this.objectSelectDD.FormattingEnabled = true;
            this.objectSelectDD.Location = new System.Drawing.Point(256, 107);
            this.objectSelectDD.Name = "objectSelectDD";
            this.objectSelectDD.Size = new System.Drawing.Size(419, 29);
            this.objectSelectDD.TabIndex = 32;
            this.objectSelectDD.SelectedIndexChanged += new System.EventHandler(this.objectSelectDD_SelectedIndexChanged);
            this.objectSelectDD.Click += new System.EventHandler(this.objectSelectDD_Click);
            // 
            // buildingSelectDD
            // 
            this.buildingSelectDD.BackColor = System.Drawing.SystemColors.Control;
            this.buildingSelectDD.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buildingSelectDD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.buildingSelectDD.ForeColor = System.Drawing.SystemColors.WindowText;
            this.buildingSelectDD.FormattingEnabled = true;
            this.buildingSelectDD.Location = new System.Drawing.Point(256, 46);
            this.buildingSelectDD.Name = "buildingSelectDD";
            this.buildingSelectDD.Size = new System.Drawing.Size(419, 29);
            this.buildingSelectDD.TabIndex = 31;
            this.buildingSelectDD.SelectedIndexChanged += new System.EventHandler(this.buildingSelectDD_SelectedIndexChanged);
            this.buildingSelectDD.Click += new System.EventHandler(this.buildingSelectDD_Click);
            // 
            // removeObjectBtn
            // 
            this.removeObjectBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.removeObjectBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.removeObjectBtn.Location = new System.Drawing.Point(919, 107);
            this.removeObjectBtn.Margin = new System.Windows.Forms.Padding(4);
            this.removeObjectBtn.Name = "removeObjectBtn";
            this.removeObjectBtn.Size = new System.Drawing.Size(211, 31);
            this.removeObjectBtn.TabIndex = 20;
            this.removeObjectBtn.Text = "Remove object";
            this.removeObjectBtn.UseVisualStyleBackColor = true;
            this.removeObjectBtn.Click += new System.EventHandler(this.removeObjectBtn_Click);
            // 
            // removeBuildingBtn
            // 
            this.removeBuildingBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.removeBuildingBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.removeBuildingBtn.Location = new System.Drawing.Point(919, 43);
            this.removeBuildingBtn.Margin = new System.Windows.Forms.Padding(4);
            this.removeBuildingBtn.Name = "removeBuildingBtn";
            this.removeBuildingBtn.Size = new System.Drawing.Size(210, 31);
            this.removeBuildingBtn.TabIndex = 19;
            this.removeBuildingBtn.Text = "Remove building";
            this.removeBuildingBtn.UseVisualStyleBackColor = true;
            this.removeBuildingBtn.Click += new System.EventHandler(this.removeBuildingBtn_Click);
            // 
            // addObjectBtn
            // 
            this.addObjectBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addObjectBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.addObjectBtn.Location = new System.Drawing.Point(702, 107);
            this.addObjectBtn.Margin = new System.Windows.Forms.Padding(4);
            this.addObjectBtn.Name = "addObjectBtn";
            this.addObjectBtn.Size = new System.Drawing.Size(210, 31);
            this.addObjectBtn.TabIndex = 11;
            this.addObjectBtn.Text = "Add object";
            this.addObjectBtn.UseVisualStyleBackColor = true;
            this.addObjectBtn.Click += new System.EventHandler(this.addObjectBtn_Click);
            // 
            // addBuildingBtn
            // 
            this.addBuildingBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addBuildingBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.addBuildingBtn.Location = new System.Drawing.Point(702, 43);
            this.addBuildingBtn.Margin = new System.Windows.Forms.Padding(4);
            this.addBuildingBtn.Name = "addBuildingBtn";
            this.addBuildingBtn.Size = new System.Drawing.Size(210, 31);
            this.addBuildingBtn.TabIndex = 8;
            this.addBuildingBtn.Text = "Add building";
            this.addBuildingBtn.UseVisualStyleBackColor = true;
            this.addBuildingBtn.Click += new System.EventHandler(this.addBuildingBtn_Click);
            // 
            // objectEditor
            // 
            this.objectEditor.BackColor = System.Drawing.SystemColors.Control;
            this.objectEditor.Controls.Add(this.modelIdOutLbl);
            this.objectEditor.Controls.Add(this.modelNameTxt);
            this.objectEditor.Controls.Add(this.label3);
            this.objectEditor.Controls.Add(this.buildingsResetTxt);
            this.objectEditor.Controls.Add(this.objectsResetTxt);
            this.objectEditor.Controls.Add(this.buildingsRemoveTxt);
            this.objectEditor.Controls.Add(this.objectsRemoveTxt);
            this.objectEditor.Controls.Add(this.resetBuildingsBtn);
            this.objectEditor.Controls.Add(this.resetObjectsBtn);
            this.objectEditor.Controls.Add(this.modelNameOutLbl);
            this.objectEditor.Controls.Add(this.modelIDTxt);
            this.objectEditor.Controls.Add(this.modelIdLbl);
            this.objectEditor.Controls.Add(this.restoreModelBtn);
            this.objectEditor.Controls.Add(this.removeModelBtn);
            this.objectEditor.Controls.Add(this.removeBuildingsBtn);
            this.objectEditor.Controls.Add(this.removeObjsBtn);
            this.objectEditor.ForeColor = System.Drawing.SystemColors.ControlText;
            this.objectEditor.Location = new System.Drawing.Point(4, 30);
            this.objectEditor.Margin = new System.Windows.Forms.Padding(4);
            this.objectEditor.Name = "objectEditor";
            this.objectEditor.Padding = new System.Windows.Forms.Padding(4);
            this.objectEditor.Size = new System.Drawing.Size(1137, 387);
            this.objectEditor.TabIndex = 0;
            this.objectEditor.Text = "Object Editor";
            // 
            // modelIdOutLbl
            // 
            this.modelIdOutLbl.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modelIdOutLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.modelIdOutLbl.Location = new System.Drawing.Point(377, 109);
            this.modelIdOutLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.modelIdOutLbl.Name = "modelIdOutLbl";
            this.modelIdOutLbl.Size = new System.Drawing.Size(289, 33);
            this.modelIdOutLbl.TabIndex = 52;
            this.modelIdOutLbl.Text = "Model Id";
            this.modelIdOutLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // modelNameTxt
            // 
            this.modelNameTxt.BackColor = System.Drawing.SystemColors.Control;
            this.modelNameTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.modelNameTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.modelNameTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.modelNameTxt.Location = new System.Drawing.Point(158, 109);
            this.modelNameTxt.Margin = new System.Windows.Forms.Padding(4);
            this.modelNameTxt.Name = "modelNameTxt";
            this.modelNameTxt.Size = new System.Drawing.Size(208, 30);
            this.modelNameTxt.TabIndex = 51;
            this.modelNameTxt.Text = "WaterTower";
            this.infoViewer.SetToolTip(this.modelNameTxt, "Model id of object like 435_01_1 for WaterTower");
            this.modelNameTxt.TextChanged += new System.EventHandler(this.modelNameTxt_TextChanged);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(8, 108);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(142, 33);
            this.label3.TabIndex = 50;
            this.label3.Text = "Model Name";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buildingsResetTxt
            // 
            this.buildingsResetTxt.BackColor = System.Drawing.SystemColors.Control;
            this.buildingsResetTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.buildingsResetTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buildingsResetTxt.Location = new System.Drawing.Point(920, 284);
            this.buildingsResetTxt.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.buildingsResetTxt.Name = "buildingsResetTxt";
            this.buildingsResetTxt.Size = new System.Drawing.Size(197, 30);
            this.buildingsResetTxt.TabIndex = 49;
            this.buildingsResetTxt.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // objectsResetTxt
            // 
            this.objectsResetTxt.BackColor = System.Drawing.SystemColors.Control;
            this.objectsResetTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.objectsResetTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.objectsResetTxt.Location = new System.Drawing.Point(921, 232);
            this.objectsResetTxt.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.objectsResetTxt.Name = "objectsResetTxt";
            this.objectsResetTxt.Size = new System.Drawing.Size(197, 30);
            this.objectsResetTxt.TabIndex = 48;
            this.objectsResetTxt.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // buildingsRemoveTxt
            // 
            this.buildingsRemoveTxt.BackColor = System.Drawing.SystemColors.Control;
            this.buildingsRemoveTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.buildingsRemoveTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buildingsRemoveTxt.Location = new System.Drawing.Point(920, 161);
            this.buildingsRemoveTxt.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.buildingsRemoveTxt.Name = "buildingsRemoveTxt";
            this.buildingsRemoveTxt.Size = new System.Drawing.Size(197, 30);
            this.buildingsRemoveTxt.TabIndex = 47;
            this.buildingsRemoveTxt.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // objectsRemoveTxt
            // 
            this.objectsRemoveTxt.BackColor = System.Drawing.SystemColors.Control;
            this.objectsRemoveTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.objectsRemoveTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.objectsRemoveTxt.Location = new System.Drawing.Point(921, 112);
            this.objectsRemoveTxt.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.objectsRemoveTxt.Name = "objectsRemoveTxt";
            this.objectsRemoveTxt.Size = new System.Drawing.Size(197, 30);
            this.objectsRemoveTxt.TabIndex = 46;
            this.objectsRemoveTxt.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // resetBuildingsBtn
            // 
            this.resetBuildingsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resetBuildingsBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.resetBuildingsBtn.Location = new System.Drawing.Point(674, 280);
            this.resetBuildingsBtn.Margin = new System.Windows.Forms.Padding(4);
            this.resetBuildingsBtn.Name = "resetBuildingsBtn";
            this.resetBuildingsBtn.Size = new System.Drawing.Size(217, 34);
            this.resetBuildingsBtn.TabIndex = 11;
            this.resetBuildingsBtn.Text = "Reset Buildings";
            this.resetBuildingsBtn.UseVisualStyleBackColor = true;
            this.resetBuildingsBtn.Click += new System.EventHandler(this.resetBuildingsBtn_Click);
            // 
            // resetObjectsBtn
            // 
            this.resetObjectsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resetObjectsBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.resetObjectsBtn.Location = new System.Drawing.Point(674, 229);
            this.resetObjectsBtn.Margin = new System.Windows.Forms.Padding(4);
            this.resetObjectsBtn.Name = "resetObjectsBtn";
            this.resetObjectsBtn.Size = new System.Drawing.Size(217, 34);
            this.resetObjectsBtn.TabIndex = 10;
            this.resetObjectsBtn.Text = "Reset Objects";
            this.resetObjectsBtn.UseVisualStyleBackColor = true;
            this.resetObjectsBtn.Click += new System.EventHandler(this.resetObjectsBtn_Click);
            // 
            // modelNameOutLbl
            // 
            this.modelNameOutLbl.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modelNameOutLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.modelNameOutLbl.Location = new System.Drawing.Point(377, 49);
            this.modelNameOutLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.modelNameOutLbl.Name = "modelNameOutLbl";
            this.modelNameOutLbl.Size = new System.Drawing.Size(289, 33);
            this.modelNameOutLbl.TabIndex = 9;
            this.modelNameOutLbl.Text = "Model Name";
            this.modelNameOutLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // modelIDTxt
            // 
            this.modelIDTxt.BackColor = System.Drawing.SystemColors.Control;
            this.modelIDTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.modelIDTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.modelIDTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.modelIDTxt.Location = new System.Drawing.Point(158, 49);
            this.modelIDTxt.Margin = new System.Windows.Forms.Padding(4);
            this.modelIDTxt.Name = "modelIDTxt";
            this.modelIDTxt.Size = new System.Drawing.Size(208, 30);
            this.modelIDTxt.TabIndex = 7;
            this.modelIDTxt.Text = "435_01_1";
            this.infoViewer.SetToolTip(this.modelIDTxt, "Model id of object like 435_01_1 for WaterTower");
            this.modelIDTxt.TextChanged += new System.EventHandler(this.objectIDTxt_TextChanged);
            this.modelIDTxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.objectIDTxt_KeyDown);
            // 
            // modelIdLbl
            // 
            this.modelIdLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.modelIdLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.modelIdLbl.Location = new System.Drawing.Point(33, 48);
            this.modelIdLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.modelIdLbl.Name = "modelIdLbl";
            this.modelIdLbl.Size = new System.Drawing.Size(117, 33);
            this.modelIdLbl.TabIndex = 6;
            this.modelIdLbl.Text = "Model Id";
            this.modelIdLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // restoreModelBtn
            // 
            this.restoreModelBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.restoreModelBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.restoreModelBtn.Location = new System.Drawing.Point(901, 49);
            this.restoreModelBtn.Margin = new System.Windows.Forms.Padding(4);
            this.restoreModelBtn.Name = "restoreModelBtn";
            this.restoreModelBtn.Size = new System.Drawing.Size(217, 31);
            this.restoreModelBtn.TabIndex = 5;
            this.restoreModelBtn.Text = "Restrore model";
            this.restoreModelBtn.UseVisualStyleBackColor = true;
            this.restoreModelBtn.Click += new System.EventHandler(this.restoreModelBtn_Click);
            // 
            // removeModelBtn
            // 
            this.removeModelBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.removeModelBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.removeModelBtn.Location = new System.Drawing.Point(674, 49);
            this.removeModelBtn.Margin = new System.Windows.Forms.Padding(4);
            this.removeModelBtn.Name = "removeModelBtn";
            this.removeModelBtn.Size = new System.Drawing.Size(217, 31);
            this.removeModelBtn.TabIndex = 5;
            this.removeModelBtn.Text = "Remove model";
            this.removeModelBtn.UseVisualStyleBackColor = true;
            this.removeModelBtn.Click += new System.EventHandler(this.removeModelBtn_Click);
            // 
            // removeBuildingsBtn
            // 
            this.removeBuildingsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.removeBuildingsBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.removeBuildingsBtn.Location = new System.Drawing.Point(674, 158);
            this.removeBuildingsBtn.Margin = new System.Windows.Forms.Padding(4);
            this.removeBuildingsBtn.Name = "removeBuildingsBtn";
            this.removeBuildingsBtn.Size = new System.Drawing.Size(217, 34);
            this.removeBuildingsBtn.TabIndex = 4;
            this.removeBuildingsBtn.Text = "Remove Buildings";
            this.removeBuildingsBtn.UseVisualStyleBackColor = true;
            this.removeBuildingsBtn.Click += new System.EventHandler(this.removeBuildingsBtn_Click);
            // 
            // removeObjsBtn
            // 
            this.removeObjsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.removeObjsBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.removeObjsBtn.Location = new System.Drawing.Point(674, 108);
            this.removeObjsBtn.Margin = new System.Windows.Forms.Padding(4);
            this.removeObjsBtn.Name = "removeObjsBtn";
            this.removeObjsBtn.Size = new System.Drawing.Size(217, 34);
            this.removeObjsBtn.TabIndex = 0;
            this.removeObjsBtn.Text = "Remove Objects";
            this.removeObjsBtn.UseVisualStyleBackColor = true;
            this.removeObjsBtn.Click += new System.EventHandler(this.removeObjsBtn_Click);
            // 
            // humanEditor
            // 
            this.humanEditor.BackColor = System.Drawing.SystemColors.Control;
            this.humanEditor.Controls.Add(this.inAirSpeedTxt);
            this.humanEditor.Controls.Add(this.upwardJumpTxt);
            this.humanEditor.Controls.Add(this.forwardJumpTxt);
            this.humanEditor.Controls.Add(this.peekTimeTxt);
            this.humanEditor.Controls.Add(this.peekCrouchTxt);
            this.humanEditor.Controls.Add(this.peekLRTxt);
            this.humanEditor.Controls.Add(this.humanViewCamTxt);
            this.humanEditor.Controls.Add(this.damageScaleFallTxt);
            this.humanEditor.Controls.Add(this.damageScaleFenceTxt);
            this.humanEditor.Controls.Add(this.teamIdTxt);
            this.humanEditor.Controls.Add(this.damageScaleTxt);
            this.humanEditor.Controls.Add(this.movementSpeedTxt);
            this.humanEditor.Controls.Add(this.updateHumanBtn);
            this.humanEditor.Controls.Add(this.resetHumanBtn);
            this.humanEditor.Controls.Add(this.inAirSpeedLbl);
            this.humanEditor.Controls.Add(this.updatePeekBtn);
            this.humanEditor.Controls.Add(this.updateHumanHealthBtn);
            this.humanEditor.Controls.Add(this.damageScaleLbl);
            this.humanEditor.Controls.Add(this.fallDamageLbl);
            this.humanEditor.Controls.Add(this.fenceDamageScaleLbl);
            this.humanEditor.Controls.Add(this.updateHumanSpeedBtn);
            this.humanEditor.Controls.Add(this.peekTimeLbl);
            this.humanEditor.Controls.Add(this.upwardJumpLbl);
            this.humanEditor.Controls.Add(this.peekCrouchLbl);
            this.humanEditor.Controls.Add(this.forwardJumpLbl);
            this.humanEditor.Controls.Add(this.humanViewCamLbl);
            this.humanEditor.Controls.Add(this.teamIdLbl);
            this.humanEditor.Controls.Add(this.peekLRLbl);
            this.humanEditor.Controls.Add(this.movementSpeedLbl);
            this.humanEditor.ForeColor = System.Drawing.SystemColors.ControlText;
            this.humanEditor.Location = new System.Drawing.Point(4, 30);
            this.humanEditor.Margin = new System.Windows.Forms.Padding(4);
            this.humanEditor.Name = "humanEditor";
            this.humanEditor.Size = new System.Drawing.Size(1137, 387);
            this.humanEditor.TabIndex = 2;
            this.humanEditor.Text = "Human Editor";
            // 
            // inAirSpeedTxt
            // 
            this.inAirSpeedTxt.BackColor = System.Drawing.SystemColors.Control;
            this.inAirSpeedTxt.DecimalPlaces = 2;
            this.inAirSpeedTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.inAirSpeedTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.inAirSpeedTxt.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.inAirSpeedTxt.Location = new System.Drawing.Point(823, 47);
            this.inAirSpeedTxt.Maximum = new decimal(new int[] {
            -805306368,
            967903254,
            1843143693,
            0});
            this.inAirSpeedTxt.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.inAirSpeedTxt.Name = "inAirSpeedTxt";
            this.inAirSpeedTxt.Size = new System.Drawing.Size(100, 30);
            this.inAirSpeedTxt.TabIndex = 76;
            this.inAirSpeedTxt.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // upwardJumpTxt
            // 
            this.upwardJumpTxt.BackColor = System.Drawing.SystemColors.Control;
            this.upwardJumpTxt.DecimalPlaces = 2;
            this.upwardJumpTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.upwardJumpTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.upwardJumpTxt.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.upwardJumpTxt.Location = new System.Drawing.Point(604, 46);
            this.upwardJumpTxt.Maximum = new decimal(new int[] {
            -805306368,
            967903254,
            1843143693,
            0});
            this.upwardJumpTxt.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.upwardJumpTxt.Name = "upwardJumpTxt";
            this.upwardJumpTxt.Size = new System.Drawing.Size(100, 30);
            this.upwardJumpTxt.TabIndex = 75;
            this.upwardJumpTxt.Value = new decimal(new int[] {
            70,
            0,
            0,
            0});
            // 
            // forwardJumpTxt
            // 
            this.forwardJumpTxt.BackColor = System.Drawing.SystemColors.Control;
            this.forwardJumpTxt.DecimalPlaces = 2;
            this.forwardJumpTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.forwardJumpTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.forwardJumpTxt.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.forwardJumpTxt.Location = new System.Drawing.Point(362, 45);
            this.forwardJumpTxt.Maximum = new decimal(new int[] {
            -805306368,
            967903254,
            1843143693,
            0});
            this.forwardJumpTxt.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.forwardJumpTxt.Name = "forwardJumpTxt";
            this.forwardJumpTxt.Size = new System.Drawing.Size(100, 30);
            this.forwardJumpTxt.TabIndex = 74;
            this.forwardJumpTxt.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // peekTimeTxt
            // 
            this.peekTimeTxt.BackColor = System.Drawing.SystemColors.Control;
            this.peekTimeTxt.DecimalPlaces = 16;
            this.peekTimeTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.peekTimeTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.peekTimeTxt.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.peekTimeTxt.Location = new System.Drawing.Point(604, 124);
            this.peekTimeTxt.Maximum = new decimal(new int[] {
            -805306368,
            967903254,
            1843143693,
            0});
            this.peekTimeTxt.Name = "peekTimeTxt";
            this.peekTimeTxt.Size = new System.Drawing.Size(100, 30);
            this.peekTimeTxt.TabIndex = 79;
            this.peekTimeTxt.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // peekCrouchTxt
            // 
            this.peekCrouchTxt.BackColor = System.Drawing.SystemColors.Control;
            this.peekCrouchTxt.DecimalPlaces = 16;
            this.peekCrouchTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.peekCrouchTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.peekCrouchTxt.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.peekCrouchTxt.Location = new System.Drawing.Point(362, 124);
            this.peekCrouchTxt.Maximum = new decimal(new int[] {
            -805306368,
            967903254,
            1843143693,
            0});
            this.peekCrouchTxt.Name = "peekCrouchTxt";
            this.peekCrouchTxt.Size = new System.Drawing.Size(100, 30);
            this.peekCrouchTxt.TabIndex = 78;
            this.peekCrouchTxt.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // peekLRTxt
            // 
            this.peekLRTxt.BackColor = System.Drawing.SystemColors.Control;
            this.peekLRTxt.DecimalPlaces = 16;
            this.peekLRTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.peekLRTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.peekLRTxt.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.peekLRTxt.Location = new System.Drawing.Point(134, 124);
            this.peekLRTxt.Maximum = new decimal(new int[] {
            -805306368,
            967903254,
            1843143693,
            0});
            this.peekLRTxt.Name = "peekLRTxt";
            this.peekLRTxt.Size = new System.Drawing.Size(100, 30);
            this.peekLRTxt.TabIndex = 77;
            this.peekLRTxt.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // humanViewCamTxt
            // 
            this.humanViewCamTxt.BackColor = System.Drawing.SystemColors.Control;
            this.humanViewCamTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.humanViewCamTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.humanViewCamTxt.Location = new System.Drawing.Point(366, 273);
            this.humanViewCamTxt.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.humanViewCamTxt.Name = "humanViewCamTxt";
            this.humanViewCamTxt.Size = new System.Drawing.Size(100, 30);
            this.humanViewCamTxt.TabIndex = 83;
            this.humanViewCamTxt.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // damageScaleFallTxt
            // 
            this.damageScaleFallTxt.BackColor = System.Drawing.SystemColors.Control;
            this.damageScaleFallTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.damageScaleFallTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.damageScaleFallTxt.Location = new System.Drawing.Point(604, 196);
            this.damageScaleFallTxt.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.damageScaleFallTxt.Name = "damageScaleFallTxt";
            this.damageScaleFallTxt.Size = new System.Drawing.Size(100, 30);
            this.damageScaleFallTxt.TabIndex = 81;
            // 
            // damageScaleFenceTxt
            // 
            this.damageScaleFenceTxt.BackColor = System.Drawing.SystemColors.Control;
            this.damageScaleFenceTxt.DecimalPlaces = 2;
            this.damageScaleFenceTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.damageScaleFenceTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.damageScaleFenceTxt.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.damageScaleFenceTxt.Location = new System.Drawing.Point(365, 195);
            this.damageScaleFenceTxt.Maximum = new decimal(new int[] {
            -805306368,
            967903254,
            1843143693,
            0});
            this.damageScaleFenceTxt.Name = "damageScaleFenceTxt";
            this.damageScaleFenceTxt.Size = new System.Drawing.Size(100, 30);
            this.damageScaleFenceTxt.TabIndex = 81;
            // 
            // teamIdTxt
            // 
            this.teamIdTxt.BackColor = System.Drawing.SystemColors.Control;
            this.teamIdTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.teamIdTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.teamIdTxt.Location = new System.Drawing.Point(135, 273);
            this.teamIdTxt.Name = "teamIdTxt";
            this.teamIdTxt.Size = new System.Drawing.Size(100, 30);
            this.teamIdTxt.TabIndex = 82;
            // 
            // damageScaleTxt
            // 
            this.damageScaleTxt.BackColor = System.Drawing.SystemColors.Control;
            this.damageScaleTxt.DecimalPlaces = 2;
            this.damageScaleTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.damageScaleTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.damageScaleTxt.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.damageScaleTxt.Location = new System.Drawing.Point(134, 195);
            this.damageScaleTxt.Maximum = new decimal(new int[] {
            -805306368,
            967903254,
            1843143693,
            0});
            this.damageScaleTxt.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.damageScaleTxt.Name = "damageScaleTxt";
            this.damageScaleTxt.Size = new System.Drawing.Size(100, 30);
            this.damageScaleTxt.TabIndex = 80;
            this.damageScaleTxt.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // movementSpeedTxt
            // 
            this.movementSpeedTxt.BackColor = System.Drawing.SystemColors.Control;
            this.movementSpeedTxt.DecimalPlaces = 2;
            this.movementSpeedTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.movementSpeedTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.movementSpeedTxt.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.movementSpeedTxt.Location = new System.Drawing.Point(140, 44);
            this.movementSpeedTxt.Maximum = new decimal(new int[] {
            -805306368,
            967903254,
            1843143693,
            0});
            this.movementSpeedTxt.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.movementSpeedTxt.Name = "movementSpeedTxt";
            this.movementSpeedTxt.Size = new System.Drawing.Size(100, 30);
            this.movementSpeedTxt.TabIndex = 73;
            this.movementSpeedTxt.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // updateHumanBtn
            // 
            this.updateHumanBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.updateHumanBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.updateHumanBtn.Location = new System.Drawing.Point(931, 273);
            this.updateHumanBtn.Margin = new System.Windows.Forms.Padding(4);
            this.updateHumanBtn.Name = "updateHumanBtn";
            this.updateHumanBtn.Size = new System.Drawing.Size(181, 31);
            this.updateHumanBtn.TabIndex = 60;
            this.updateHumanBtn.Text = "Update Human";
            this.updateHumanBtn.UseVisualStyleBackColor = true;
            this.updateHumanBtn.Click += new System.EventHandler(this.updateTeamIdBtn_Click);
            // 
            // resetHumanBtn
            // 
            this.resetHumanBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resetHumanBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.resetHumanBtn.Location = new System.Drawing.Point(931, 347);
            this.resetHumanBtn.Margin = new System.Windows.Forms.Padding(4);
            this.resetHumanBtn.Name = "resetHumanBtn";
            this.resetHumanBtn.Size = new System.Drawing.Size(181, 31);
            this.resetHumanBtn.TabIndex = 59;
            this.resetHumanBtn.Text = "Reset Human";
            this.resetHumanBtn.UseVisualStyleBackColor = true;
            this.resetHumanBtn.Click += new System.EventHandler(this.resetHumanBtn_Click);
            // 
            // inAirSpeedLbl
            // 
            this.inAirSpeedLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.inAirSpeedLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.inAirSpeedLbl.Location = new System.Drawing.Point(713, 46);
            this.inAirSpeedLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.inAirSpeedLbl.Name = "inAirSpeedLbl";
            this.inAirSpeedLbl.Size = new System.Drawing.Size(107, 33);
            this.inAirSpeedLbl.TabIndex = 52;
            this.inAirSpeedLbl.Text = "In Air";
            this.inAirSpeedLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // updatePeekBtn
            // 
            this.updatePeekBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.updatePeekBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.updatePeekBtn.Location = new System.Drawing.Point(931, 124);
            this.updatePeekBtn.Margin = new System.Windows.Forms.Padding(4);
            this.updatePeekBtn.Name = "updatePeekBtn";
            this.updatePeekBtn.Size = new System.Drawing.Size(181, 31);
            this.updatePeekBtn.TabIndex = 51;
            this.updatePeekBtn.Text = "Update Peek";
            this.updatePeekBtn.UseVisualStyleBackColor = true;
            this.updatePeekBtn.Click += new System.EventHandler(this.updateHumanPeekBtn_Click);
            // 
            // updateHumanHealthBtn
            // 
            this.updateHumanHealthBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.updateHumanHealthBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.updateHumanHealthBtn.Location = new System.Drawing.Point(931, 197);
            this.updateHumanHealthBtn.Margin = new System.Windows.Forms.Padding(4);
            this.updateHumanHealthBtn.Name = "updateHumanHealthBtn";
            this.updateHumanHealthBtn.Size = new System.Drawing.Size(181, 31);
            this.updateHumanHealthBtn.TabIndex = 51;
            this.updateHumanHealthBtn.Text = "Update Health";
            this.updateHumanHealthBtn.UseVisualStyleBackColor = true;
            this.updateHumanHealthBtn.Click += new System.EventHandler(this.updateHumanHealthBtn_Click);
            // 
            // damageScaleLbl
            // 
            this.damageScaleLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.damageScaleLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.damageScaleLbl.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.damageScaleLbl.Location = new System.Drawing.Point(23, 195);
            this.damageScaleLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.damageScaleLbl.Name = "damageScaleLbl";
            this.damageScaleLbl.Size = new System.Drawing.Size(110, 33);
            this.damageScaleLbl.TabIndex = 47;
            this.damageScaleLbl.Text = "Human";
            this.damageScaleLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fallDamageLbl
            // 
            this.fallDamageLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.fallDamageLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.fallDamageLbl.Location = new System.Drawing.Point(479, 196);
            this.fallDamageLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.fallDamageLbl.Name = "fallDamageLbl";
            this.fallDamageLbl.Size = new System.Drawing.Size(96, 33);
            this.fallDamageLbl.TabIndex = 47;
            this.fallDamageLbl.Text = "Fall";
            this.fallDamageLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fenceDamageScaleLbl
            // 
            this.fenceDamageScaleLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.fenceDamageScaleLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.fenceDamageScaleLbl.Location = new System.Drawing.Point(237, 195);
            this.fenceDamageScaleLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.fenceDamageScaleLbl.Name = "fenceDamageScaleLbl";
            this.fenceDamageScaleLbl.Size = new System.Drawing.Size(96, 33);
            this.fenceDamageScaleLbl.TabIndex = 47;
            this.fenceDamageScaleLbl.Text = "Fence";
            this.fenceDamageScaleLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // updateHumanSpeedBtn
            // 
            this.updateHumanSpeedBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.updateHumanSpeedBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.updateHumanSpeedBtn.Location = new System.Drawing.Point(931, 48);
            this.updateHumanSpeedBtn.Margin = new System.Windows.Forms.Padding(4);
            this.updateHumanSpeedBtn.Name = "updateHumanSpeedBtn";
            this.updateHumanSpeedBtn.Size = new System.Drawing.Size(181, 31);
            this.updateHumanSpeedBtn.TabIndex = 44;
            this.updateHumanSpeedBtn.Text = "Update Speed";
            this.updateHumanSpeedBtn.UseVisualStyleBackColor = true;
            this.updateHumanSpeedBtn.Click += new System.EventHandler(this.updateHumanSpeedBtn_Click);
            // 
            // peekTimeLbl
            // 
            this.peekTimeLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.peekTimeLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.peekTimeLbl.Location = new System.Drawing.Point(479, 122);
            this.peekTimeLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.peekTimeLbl.Name = "peekTimeLbl";
            this.peekTimeLbl.Size = new System.Drawing.Size(129, 33);
            this.peekTimeLbl.TabIndex = 12;
            this.peekTimeLbl.Text = "Time";
            this.peekTimeLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // upwardJumpLbl
            // 
            this.upwardJumpLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.upwardJumpLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.upwardJumpLbl.Location = new System.Drawing.Point(479, 45);
            this.upwardJumpLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.upwardJumpLbl.Name = "upwardJumpLbl";
            this.upwardJumpLbl.Size = new System.Drawing.Size(129, 33);
            this.upwardJumpLbl.TabIndex = 12;
            this.upwardJumpLbl.Text = "Upward";
            this.upwardJumpLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // peekCrouchLbl
            // 
            this.peekCrouchLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.peekCrouchLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.peekCrouchLbl.Location = new System.Drawing.Point(250, 122);
            this.peekCrouchLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.peekCrouchLbl.Name = "peekCrouchLbl";
            this.peekCrouchLbl.Size = new System.Drawing.Size(117, 33);
            this.peekCrouchLbl.TabIndex = 10;
            this.peekCrouchLbl.Text = "Crouch";
            this.peekCrouchLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // forwardJumpLbl
            // 
            this.forwardJumpLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.forwardJumpLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.forwardJumpLbl.Location = new System.Drawing.Point(237, 44);
            this.forwardJumpLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.forwardJumpLbl.Name = "forwardJumpLbl";
            this.forwardJumpLbl.Size = new System.Drawing.Size(117, 33);
            this.forwardJumpLbl.TabIndex = 10;
            this.forwardJumpLbl.Text = "Forward";
            this.forwardJumpLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // humanViewCamLbl
            // 
            this.humanViewCamLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.humanViewCamLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.humanViewCamLbl.Location = new System.Drawing.Point(237, 269);
            this.humanViewCamLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.humanViewCamLbl.Name = "humanViewCamLbl";
            this.humanViewCamLbl.Size = new System.Drawing.Size(117, 33);
            this.humanViewCamLbl.TabIndex = 8;
            this.humanViewCamLbl.Text = "View Cam";
            this.humanViewCamLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // teamIdLbl
            // 
            this.teamIdLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.teamIdLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.teamIdLbl.Location = new System.Drawing.Point(8, 269);
            this.teamIdLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.teamIdLbl.Name = "teamIdLbl";
            this.teamIdLbl.Size = new System.Drawing.Size(114, 33);
            this.teamIdLbl.TabIndex = 8;
            this.teamIdLbl.Text = "Team Id";
            this.teamIdLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // peekLRLbl
            // 
            this.peekLRLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.peekLRLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.peekLRLbl.Location = new System.Drawing.Point(8, 121);
            this.peekLRLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.peekLRLbl.Name = "peekLRLbl";
            this.peekLRLbl.Size = new System.Drawing.Size(125, 33);
            this.peekLRLbl.TabIndex = 8;
            this.peekLRLbl.Text = "Left Right";
            this.peekLRLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // movementSpeedLbl
            // 
            this.movementSpeedLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.movementSpeedLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.movementSpeedLbl.Location = new System.Drawing.Point(8, 44);
            this.movementSpeedLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.movementSpeedLbl.Name = "movementSpeedLbl";
            this.movementSpeedLbl.Size = new System.Drawing.Size(125, 33);
            this.movementSpeedLbl.TabIndex = 8;
            this.movementSpeedLbl.Text = "Movement";
            this.movementSpeedLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // aiEditor
            // 
            this.aiEditor.BackColor = System.Drawing.SystemColors.Control;
            this.aiEditor.Controls.Add(this.aiEditorTabs);
            this.aiEditor.ForeColor = System.Drawing.SystemColors.ControlText;
            this.aiEditor.Location = new System.Drawing.Point(4, 30);
            this.aiEditor.Margin = new System.Windows.Forms.Padding(4);
            this.aiEditor.Name = "aiEditor";
            this.aiEditor.Size = new System.Drawing.Size(1137, 387);
            this.aiEditor.TabIndex = 8;
            this.aiEditor.Text = "A.I Editor";
            // 
            // aiEditorTabs
            // 
            this.aiEditorTabs.Controls.Add(this.aiEditorMainTab);
            this.aiEditorTabs.Controls.Add(this.aiJSONEditor);
            this.aiEditorTabs.Controls.Add(this.aiScriptEditor);
            this.aiEditorTabs.Controls.Add(this.aiPatrolPathEditor);
            this.aiEditorTabs.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aiEditorTabs.Location = new System.Drawing.Point(0, 0);
            this.aiEditorTabs.Margin = new System.Windows.Forms.Padding(4);
            this.aiEditorTabs.Name = "aiEditorTabs";
            this.aiEditorTabs.SelectedIndex = 0;
            this.aiEditorTabs.Size = new System.Drawing.Size(1137, 408);
            this.aiEditorTabs.TabIndex = 85;
            this.aiEditorTabs.Selected += new System.Windows.Forms.TabControlEventHandler(this.aiEditorTabs_Selected);
            // 
            // aiEditorMainTab
            // 
            this.aiEditorMainTab.BackColor = System.Drawing.SystemColors.Control;
            this.aiEditorMainTab.Controls.Add(this.teamIdText);
            this.aiEditorMainTab.Controls.Add(this.teamIdLabel);
            this.aiEditorMainTab.Controls.Add(this.removeAllAiCb);
            this.aiEditorMainTab.Controls.Add(this.maxSpawnsTxt);
            this.aiEditorMainTab.Controls.Add(this.aiCountTxt);
            this.aiEditorMainTab.Controls.Add(this.removeAiBtn);
            this.aiEditorMainTab.Controls.Add(this.graphAreaAiLbl);
            this.aiEditorMainTab.Controls.Add(this.taskDetectionAiCb);
            this.aiEditorMainTab.Controls.Add(this.customAiCb);
            this.aiEditorMainTab.Controls.Add(this.maxSpawnsLbl);
            this.aiEditorMainTab.Controls.Add(this.aiFriendlyCb);
            this.aiEditorMainTab.Controls.Add(this.aiAdvanceViewCb);
            this.aiEditorMainTab.Controls.Add(this.guardGeneratorCb);
            this.aiEditorMainTab.Controls.Add(this.aiInvincibleCb);
            this.aiEditorMainTab.Controls.Add(this.aiGraphIdDD);
            this.aiEditorMainTab.Controls.Add(this.aiTypeDD);
            this.aiEditorMainTab.Controls.Add(this.aiWeaponDD);
            this.aiEditorMainTab.Controls.Add(this.graphIdAiLbl);
            this.aiEditorMainTab.Controls.Add(this.aiTypeLbl);
            this.aiEditorMainTab.Controls.Add(this.weaponAiLbl);
            this.aiEditorMainTab.Controls.Add(this.weaponAIImgBox);
            this.aiEditorMainTab.Controls.Add(this.aiModelSelectDD);
            this.aiEditorMainTab.Controls.Add(this.aiImgBox);
            this.aiEditorMainTab.Controls.Add(this.aiCountLbl);
            this.aiEditorMainTab.Controls.Add(this.aiModelNameLbl);
            this.aiEditorMainTab.Controls.Add(this.aiModelLbl);
            this.aiEditorMainTab.Controls.Add(this.saveAIBtn);
            this.aiEditorMainTab.Controls.Add(this.addAiBtn);
            this.aiEditorMainTab.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.aiEditorMainTab.ForeColor = System.Drawing.SystemColors.ControlText;
            this.aiEditorMainTab.Location = new System.Drawing.Point(4, 26);
            this.aiEditorMainTab.Margin = new System.Windows.Forms.Padding(4);
            this.aiEditorMainTab.Name = "aiEditorMainTab";
            this.aiEditorMainTab.Size = new System.Drawing.Size(1129, 378);
            this.aiEditorMainTab.TabIndex = 0;
            this.aiEditorMainTab.Text = "Main Editor";
            // 
            // teamIdText
            // 
            this.teamIdText.BackColor = System.Drawing.SystemColors.Control;
            this.teamIdText.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.teamIdText.ForeColor = System.Drawing.SystemColors.ControlText;
            this.teamIdText.Location = new System.Drawing.Point(684, 260);
            this.teamIdText.Name = "teamIdText";
            this.teamIdText.Size = new System.Drawing.Size(68, 30);
            this.teamIdText.TabIndex = 111;
            // 
            // teamIdLabel
            // 
            this.teamIdLabel.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.teamIdLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.teamIdLabel.Location = new System.Drawing.Point(533, 260);
            this.teamIdLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.teamIdLabel.Name = "teamIdLabel";
            this.teamIdLabel.Size = new System.Drawing.Size(119, 33);
            this.teamIdLabel.TabIndex = 110;
            this.teamIdLabel.Text = "Team Id";
            this.teamIdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // removeAllAiCb
            // 
            this.removeAllAiCb.Font = new System.Drawing.Font("Century Gothic", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.removeAllAiCb.Location = new System.Drawing.Point(974, 297);
            this.removeAllAiCb.Margin = new System.Windows.Forms.Padding(4);
            this.removeAllAiCb.Name = "removeAllAiCb";
            this.removeAllAiCb.Size = new System.Drawing.Size(117, 20);
            this.removeAllAiCb.TabIndex = 109;
            this.removeAllAiCb.Text = "All Soldiers";
            this.removeAllAiCb.UseVisualStyleBackColor = true;
            // 
            // maxSpawnsTxt
            // 
            this.maxSpawnsTxt.BackColor = System.Drawing.SystemColors.Control;
            this.maxSpawnsTxt.Enabled = false;
            this.maxSpawnsTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.maxSpawnsTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.maxSpawnsTxt.Location = new System.Drawing.Point(1040, 260);
            this.maxSpawnsTxt.Name = "maxSpawnsTxt";
            this.maxSpawnsTxt.Size = new System.Drawing.Size(68, 30);
            this.maxSpawnsTxt.TabIndex = 108;
            this.maxSpawnsTxt.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // aiCountTxt
            // 
            this.aiCountTxt.BackColor = System.Drawing.SystemColors.Control;
            this.aiCountTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.aiCountTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.aiCountTxt.Location = new System.Drawing.Point(874, 260);
            this.aiCountTxt.Name = "aiCountTxt";
            this.aiCountTxt.Size = new System.Drawing.Size(68, 30);
            this.aiCountTxt.TabIndex = 107;
            this.aiCountTxt.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // removeAiBtn
            // 
            this.removeAiBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.removeAiBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.removeAiBtn.Location = new System.Drawing.Point(974, 317);
            this.removeAiBtn.Margin = new System.Windows.Forms.Padding(4);
            this.removeAiBtn.Name = "removeAiBtn";
            this.removeAiBtn.Size = new System.Drawing.Size(134, 31);
            this.removeAiBtn.TabIndex = 106;
            this.removeAiBtn.Text = "Remove A.I";
            this.removeAiBtn.UseVisualStyleBackColor = true;
            this.removeAiBtn.Click += new System.EventHandler(this.removeAiBtn_Click);
            // 
            // graphAreaAiLbl
            // 
            this.graphAreaAiLbl.BackColor = System.Drawing.SystemColors.Control;
            this.graphAreaAiLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.graphAreaAiLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.graphAreaAiLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.graphAreaAiLbl.Location = new System.Drawing.Point(775, 189);
            this.graphAreaAiLbl.Margin = new System.Windows.Forms.Padding(4);
            this.graphAreaAiLbl.Name = "graphAreaAiLbl";
            this.graphAreaAiLbl.ReadOnly = true;
            this.graphAreaAiLbl.Size = new System.Drawing.Size(328, 30);
            this.graphAreaAiLbl.TabIndex = 105;
            this.graphAreaAiLbl.Text = "Graph Area";
            // 
            // taskDetectionAiCb
            // 
            this.taskDetectionAiCb.AutoSize = true;
            this.taskDetectionAiCb.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.taskDetectionAiCb.Location = new System.Drawing.Point(221, 135);
            this.taskDetectionAiCb.Margin = new System.Windows.Forms.Padding(4);
            this.taskDetectionAiCb.Name = "taskDetectionAiCb";
            this.taskDetectionAiCb.Size = new System.Drawing.Size(157, 25);
            this.taskDetectionAiCb.TabIndex = 104;
            this.taskDetectionAiCb.Text = "Task Detection";
            this.taskDetectionAiCb.UseVisualStyleBackColor = true;
            // 
            // customAiCb
            // 
            this.customAiCb.AutoSize = true;
            this.customAiCb.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customAiCb.Location = new System.Drawing.Point(221, 109);
            this.customAiCb.Margin = new System.Windows.Forms.Padding(4);
            this.customAiCb.Name = "customAiCb";
            this.customAiCb.Size = new System.Drawing.Size(124, 25);
            this.customAiCb.TabIndex = 103;
            this.customAiCb.Text = "Custom A.I";
            this.customAiCb.UseVisualStyleBackColor = true;
            this.customAiCb.Click += new System.EventHandler(this.customAiCb_CheckedChanged);
            // 
            // maxSpawnsLbl
            // 
            this.maxSpawnsLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.maxSpawnsLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.maxSpawnsLbl.Location = new System.Drawing.Point(949, 257);
            this.maxSpawnsLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.maxSpawnsLbl.Name = "maxSpawnsLbl";
            this.maxSpawnsLbl.Size = new System.Drawing.Size(84, 33);
            this.maxSpawnsLbl.TabIndex = 102;
            this.maxSpawnsLbl.Text = "Spawns ";
            this.maxSpawnsLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // aiFriendlyCb
            // 
            this.aiFriendlyCb.AutoSize = true;
            this.aiFriendlyCb.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aiFriendlyCb.Location = new System.Drawing.Point(221, 57);
            this.aiFriendlyCb.Margin = new System.Windows.Forms.Padding(4);
            this.aiFriendlyCb.Name = "aiFriendlyCb";
            this.aiFriendlyCb.Size = new System.Drawing.Size(92, 25);
            this.aiFriendlyCb.TabIndex = 100;
            this.aiFriendlyCb.Text = "Friendly";
            this.aiFriendlyCb.UseVisualStyleBackColor = true;
            this.aiFriendlyCb.CheckedChanged += new System.EventHandler(this.aiFriendlyCb_CheckedChanged);
            // 
            // aiAdvanceViewCb
            // 
            this.aiAdvanceViewCb.AutoSize = true;
            this.aiAdvanceViewCb.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aiAdvanceViewCb.Location = new System.Drawing.Point(221, 31);
            this.aiAdvanceViewCb.Margin = new System.Windows.Forms.Padding(4);
            this.aiAdvanceViewCb.Name = "aiAdvanceViewCb";
            this.aiAdvanceViewCb.Size = new System.Drawing.Size(158, 25);
            this.aiAdvanceViewCb.TabIndex = 99;
            this.aiAdvanceViewCb.Text = "Advance View";
            this.aiAdvanceViewCb.UseVisualStyleBackColor = true;
            // 
            // guardGeneratorCb
            // 
            this.guardGeneratorCb.AutoSize = true;
            this.guardGeneratorCb.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guardGeneratorCb.Location = new System.Drawing.Point(221, 83);
            this.guardGeneratorCb.Margin = new System.Windows.Forms.Padding(4);
            this.guardGeneratorCb.Name = "guardGeneratorCb";
            this.guardGeneratorCb.Size = new System.Drawing.Size(178, 25);
            this.guardGeneratorCb.TabIndex = 98;
            this.guardGeneratorCb.Text = "Guard Generator";
            this.guardGeneratorCb.UseVisualStyleBackColor = true;
            this.guardGeneratorCb.CheckedChanged += new System.EventHandler(this.guardGeneratorCb_CheckedChanged);
            // 
            // aiInvincibleCb
            // 
            this.aiInvincibleCb.AutoSize = true;
            this.aiInvincibleCb.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aiInvincibleCb.Location = new System.Drawing.Point(221, 5);
            this.aiInvincibleCb.Margin = new System.Windows.Forms.Padding(4);
            this.aiInvincibleCb.Name = "aiInvincibleCb";
            this.aiInvincibleCb.Size = new System.Drawing.Size(111, 25);
            this.aiInvincibleCb.TabIndex = 101;
            this.aiInvincibleCb.Text = "Invincible";
            this.aiInvincibleCb.UseVisualStyleBackColor = true;
            // 
            // aiGraphIdDD
            // 
            this.aiGraphIdDD.BackColor = System.Drawing.SystemColors.Control;
            this.aiGraphIdDD.Cursor = System.Windows.Forms.Cursors.Hand;
            this.aiGraphIdDD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.aiGraphIdDD.ForeColor = System.Drawing.SystemColors.WindowText;
            this.aiGraphIdDD.FormattingEnabled = true;
            this.aiGraphIdDD.Location = new System.Drawing.Point(684, 188);
            this.aiGraphIdDD.Name = "aiGraphIdDD";
            this.aiGraphIdDD.Size = new System.Drawing.Size(73, 29);
            this.aiGraphIdDD.TabIndex = 97;
            this.aiGraphIdDD.SelectedIndexChanged += new System.EventHandler(this.aiGraphIdDD_SelectedValueChanged);
            // 
            // aiTypeDD
            // 
            this.aiTypeDD.BackColor = System.Drawing.SystemColors.Control;
            this.aiTypeDD.Cursor = System.Windows.Forms.Cursors.Hand;
            this.aiTypeDD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.aiTypeDD.ForeColor = System.Drawing.SystemColors.WindowText;
            this.aiTypeDD.FormattingEnabled = true;
            this.aiTypeDD.Location = new System.Drawing.Point(684, 125);
            this.aiTypeDD.Name = "aiTypeDD";
            this.aiTypeDD.Size = new System.Drawing.Size(424, 29);
            this.aiTypeDD.TabIndex = 96;
            this.aiTypeDD.SelectedIndexChanged += new System.EventHandler(this.aiTypeDD_SelectedIndexChanged);
            // 
            // aiWeaponDD
            // 
            this.aiWeaponDD.BackColor = System.Drawing.SystemColors.Control;
            this.aiWeaponDD.Cursor = System.Windows.Forms.Cursors.Hand;
            this.aiWeaponDD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.aiWeaponDD.ForeColor = System.Drawing.SystemColors.WindowText;
            this.aiWeaponDD.FormattingEnabled = true;
            this.aiWeaponDD.Location = new System.Drawing.Point(684, 67);
            this.aiWeaponDD.Name = "aiWeaponDD";
            this.aiWeaponDD.Size = new System.Drawing.Size(424, 29);
            this.aiWeaponDD.TabIndex = 95;
            this.aiWeaponDD.SelectedValueChanged += new System.EventHandler(this.weaponAiDD_SelectedValueChanged);
            // 
            // graphIdAiLbl
            // 
            this.graphIdAiLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.graphIdAiLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.graphIdAiLbl.Location = new System.Drawing.Point(498, 185);
            this.graphIdAiLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.graphIdAiLbl.Name = "graphIdAiLbl";
            this.graphIdAiLbl.Size = new System.Drawing.Size(173, 33);
            this.graphIdAiLbl.TabIndex = 94;
            this.graphIdAiLbl.Text = "Graph Id ";
            this.graphIdAiLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // aiTypeLbl
            // 
            this.aiTypeLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.aiTypeLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.aiTypeLbl.Location = new System.Drawing.Point(498, 125);
            this.aiTypeLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.aiTypeLbl.Name = "aiTypeLbl";
            this.aiTypeLbl.Size = new System.Drawing.Size(173, 33);
            this.aiTypeLbl.TabIndex = 93;
            this.aiTypeLbl.Text = "Type ";
            this.aiTypeLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // weaponAiLbl
            // 
            this.weaponAiLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.weaponAiLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.weaponAiLbl.Location = new System.Drawing.Point(498, 63);
            this.weaponAiLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.weaponAiLbl.Name = "weaponAiLbl";
            this.weaponAiLbl.Size = new System.Drawing.Size(173, 33);
            this.weaponAiLbl.TabIndex = 92;
            this.weaponAiLbl.Text = "Weapon ";
            this.weaponAiLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // weaponAIImgBox
            // 
            this.weaponAIImgBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.weaponAIImgBox.Location = new System.Drawing.Point(211, 167);
            this.weaponAIImgBox.Margin = new System.Windows.Forms.Padding(4);
            this.weaponAIImgBox.Name = "weaponAIImgBox";
            this.weaponAIImgBox.Size = new System.Drawing.Size(199, 184);
            this.weaponAIImgBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.weaponAIImgBox.TabIndex = 91;
            this.weaponAIImgBox.TabStop = false;
            // 
            // aiModelSelectDD
            // 
            this.aiModelSelectDD.BackColor = System.Drawing.SystemColors.Control;
            this.aiModelSelectDD.Cursor = System.Windows.Forms.Cursors.Hand;
            this.aiModelSelectDD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.aiModelSelectDD.ForeColor = System.Drawing.SystemColors.WindowText;
            this.aiModelSelectDD.FormattingEnabled = true;
            this.aiModelSelectDD.Location = new System.Drawing.Point(684, 6);
            this.aiModelSelectDD.Name = "aiModelSelectDD";
            this.aiModelSelectDD.Size = new System.Drawing.Size(419, 29);
            this.aiModelSelectDD.TabIndex = 90;
            this.aiModelSelectDD.SelectedValueChanged += new System.EventHandler(this.aiModelSelectDD_SelectedValueChanged);
            // 
            // aiImgBox
            // 
            this.aiImgBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.aiImgBox.Location = new System.Drawing.Point(4, 29);
            this.aiImgBox.Margin = new System.Windows.Forms.Padding(4);
            this.aiImgBox.Name = "aiImgBox";
            this.aiImgBox.Size = new System.Drawing.Size(199, 322);
            this.aiImgBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.aiImgBox.TabIndex = 89;
            this.aiImgBox.TabStop = false;
            // 
            // aiCountLbl
            // 
            this.aiCountLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.aiCountLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.aiCountLbl.Location = new System.Drawing.Point(747, 259);
            this.aiCountLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.aiCountLbl.Name = "aiCountLbl";
            this.aiCountLbl.Size = new System.Drawing.Size(119, 33);
            this.aiCountLbl.TabIndex = 87;
            this.aiCountLbl.Text = "AI Count";
            this.aiCountLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // aiModelNameLbl
            // 
            this.aiModelNameLbl.Font = new System.Drawing.Font("Century Gothic", 13F);
            this.aiModelNameLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.aiModelNameLbl.Location = new System.Drawing.Point(0, 0);
            this.aiModelNameLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.aiModelNameLbl.Name = "aiModelNameLbl";
            this.aiModelNameLbl.Size = new System.Drawing.Size(203, 28);
            this.aiModelNameLbl.TabIndex = 86;
            this.aiModelNameLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // aiModelLbl
            // 
            this.aiModelLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.aiModelLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.aiModelLbl.Location = new System.Drawing.Point(493, 2);
            this.aiModelLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.aiModelLbl.Name = "aiModelLbl";
            this.aiModelLbl.Size = new System.Drawing.Size(173, 33);
            this.aiModelLbl.TabIndex = 88;
            this.aiModelLbl.Text = "Model";
            this.aiModelLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // saveAIBtn
            // 
            this.saveAIBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveAIBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.saveAIBtn.Location = new System.Drawing.Point(826, 317);
            this.saveAIBtn.Margin = new System.Windows.Forms.Padding(4);
            this.saveAIBtn.Name = "saveAIBtn";
            this.saveAIBtn.Size = new System.Drawing.Size(134, 31);
            this.saveAIBtn.TabIndex = 85;
            this.saveAIBtn.Text = "Save A.I";
            this.saveAIBtn.UseVisualStyleBackColor = true;
            this.saveAIBtn.Click += new System.EventHandler(this.saveAIBtn_Click);
            // 
            // addAiBtn
            // 
            this.addAiBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addAiBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.addAiBtn.Location = new System.Drawing.Point(684, 317);
            this.addAiBtn.Margin = new System.Windows.Forms.Padding(4);
            this.addAiBtn.Name = "addAiBtn";
            this.addAiBtn.Size = new System.Drawing.Size(134, 31);
            this.addAiBtn.TabIndex = 85;
            this.addAiBtn.Text = "Add A.I";
            this.addAiBtn.UseVisualStyleBackColor = true;
            this.addAiBtn.Click += new System.EventHandler(this.addAiBtn_Click);
            // 
            // aiJSONEditor
            // 
            this.aiJSONEditor.BackColor = System.Drawing.SystemColors.Control;
            this.aiJSONEditor.Controls.Add(this.aiJsonAutoFmtCb);
            this.aiJSONEditor.Controls.Add(this.aiJsonEditModeCb);
            this.aiJSONEditor.Controls.Add(this.aiJsonClearDataCb);
            this.aiJSONEditor.Controls.Add(this.aiJsonEditorTxt);
            this.aiJSONEditor.Controls.Add(this.aiJsonSaveBtn);
            this.aiJSONEditor.Controls.Add(this.aiJsonLoadBtn);
            this.aiJSONEditor.Controls.Add(this.aiJsonAddAiBtn);
            this.aiJSONEditor.Controls.Add(this.aiFileNameTxt);
            this.aiJSONEditor.Controls.Add(this.aiFileSizeTxt);
            this.aiJSONEditor.Controls.Add(this.aiJsonVersionTxt);
            this.aiJSONEditor.Controls.Add(this.aiJsonSaveAiBtn);
            this.aiJSONEditor.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aiJSONEditor.ForeColor = System.Drawing.SystemColors.ControlText;
            this.aiJSONEditor.Location = new System.Drawing.Point(4, 26);
            this.aiJSONEditor.Name = "aiJSONEditor";
            this.aiJSONEditor.Padding = new System.Windows.Forms.Padding(3);
            this.aiJSONEditor.Size = new System.Drawing.Size(1129, 378);
            this.aiJSONEditor.TabIndex = 1;
            this.aiJSONEditor.Text = "JSON Editor";
            // 
            // aiJsonAutoFmtCb
            // 
            this.aiJsonAutoFmtCb.AutoSize = true;
            this.aiJsonAutoFmtCb.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aiJsonAutoFmtCb.Location = new System.Drawing.Point(1046, 219);
            this.aiJsonAutoFmtCb.Margin = new System.Windows.Forms.Padding(4);
            this.aiJsonAutoFmtCb.Name = "aiJsonAutoFmtCb";
            this.aiJsonAutoFmtCb.Size = new System.Drawing.Size(79, 23);
            this.aiJsonAutoFmtCb.TabIndex = 64;
            this.aiJsonAutoFmtCb.Text = "Format";
            this.aiJsonAutoFmtCb.UseVisualStyleBackColor = true;
            // 
            // aiJsonEditModeCb
            // 
            this.aiJsonEditModeCb.AutoSize = true;
            this.aiJsonEditModeCb.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aiJsonEditModeCb.Location = new System.Drawing.Point(873, 219);
            this.aiJsonEditModeCb.Margin = new System.Windows.Forms.Padding(4);
            this.aiJsonEditModeCb.Name = "aiJsonEditModeCb";
            this.aiJsonEditModeCb.Size = new System.Drawing.Size(57, 23);
            this.aiJsonEditModeCb.TabIndex = 65;
            this.aiJsonEditModeCb.Text = "Edit";
            this.aiJsonEditModeCb.UseVisualStyleBackColor = true;
            this.aiJsonEditModeCb.CheckedChanged += new System.EventHandler(this.aiJsonEditModeCb_CheckedChanged);
            // 
            // aiJsonClearDataCb
            // 
            this.aiJsonClearDataCb.AutoSize = true;
            this.aiJsonClearDataCb.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aiJsonClearDataCb.Location = new System.Drawing.Point(956, 219);
            this.aiJsonClearDataCb.Margin = new System.Windows.Forms.Padding(4);
            this.aiJsonClearDataCb.Name = "aiJsonClearDataCb";
            this.aiJsonClearDataCb.Size = new System.Drawing.Size(68, 23);
            this.aiJsonClearDataCb.TabIndex = 65;
            this.aiJsonClearDataCb.Text = "Clear";
            this.aiJsonClearDataCb.UseVisualStyleBackColor = true;
            // 
            // aiJsonEditorTxt
            // 
            this.aiJsonEditorTxt.AutoWordSelection = true;
            this.aiJsonEditorTxt.BackColor = System.Drawing.SystemColors.Control;
            this.aiJsonEditorTxt.Font = new System.Drawing.Font("Century Gothic", 10.15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aiJsonEditorTxt.ForeColor = System.Drawing.Color.White;
            this.aiJsonEditorTxt.Location = new System.Drawing.Point(1, -2);
            this.aiJsonEditorTxt.Name = "aiJsonEditorTxt";
            this.aiJsonEditorTxt.ReadOnly = true;
            this.aiJsonEditorTxt.Size = new System.Drawing.Size(865, 360);
            this.aiJsonEditorTxt.TabIndex = 63;
            this.aiJsonEditorTxt.Text = "";
            this.aiJsonEditorTxt.TextChanged += new System.EventHandler(this.aiJsonEditorTxt_TextChanged);
            this.aiJsonEditorTxt.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.aiJsonEditorTxt_PreviewKeyDown);
            // 
            // aiJsonSaveBtn
            // 
            this.aiJsonSaveBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.aiJsonSaveBtn.Font = new System.Drawing.Font("Century Gothic", 10.2F);
            this.aiJsonSaveBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.aiJsonSaveBtn.Location = new System.Drawing.Point(871, 170);
            this.aiJsonSaveBtn.Margin = new System.Windows.Forms.Padding(4);
            this.aiJsonSaveBtn.Name = "aiJsonSaveBtn";
            this.aiJsonSaveBtn.Size = new System.Drawing.Size(254, 41);
            this.aiJsonSaveBtn.TabIndex = 60;
            this.aiJsonSaveBtn.Text = "Save JSON";
            this.aiJsonSaveBtn.UseVisualStyleBackColor = true;
            this.aiJsonSaveBtn.Click += new System.EventHandler(this.aiJsonSaveBtn_Click);
            // 
            // aiJsonLoadBtn
            // 
            this.aiJsonLoadBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.aiJsonLoadBtn.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aiJsonLoadBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.aiJsonLoadBtn.Location = new System.Drawing.Point(871, 121);
            this.aiJsonLoadBtn.Margin = new System.Windows.Forms.Padding(4);
            this.aiJsonLoadBtn.Name = "aiJsonLoadBtn";
            this.aiJsonLoadBtn.Size = new System.Drawing.Size(254, 41);
            this.aiJsonLoadBtn.TabIndex = 61;
            this.aiJsonLoadBtn.Text = "Load JSON";
            this.aiJsonLoadBtn.UseVisualStyleBackColor = true;
            this.aiJsonLoadBtn.Click += new System.EventHandler(this.aiJsonLoadBtn_Click);
            // 
            // aiJsonAddAiBtn
            // 
            this.aiJsonAddAiBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.aiJsonAddAiBtn.Font = new System.Drawing.Font("Century Gothic", 10.2F);
            this.aiJsonAddAiBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.aiJsonAddAiBtn.Location = new System.Drawing.Point(873, 262);
            this.aiJsonAddAiBtn.Margin = new System.Windows.Forms.Padding(4);
            this.aiJsonAddAiBtn.Name = "aiJsonAddAiBtn";
            this.aiJsonAddAiBtn.Size = new System.Drawing.Size(254, 41);
            this.aiJsonAddAiBtn.TabIndex = 62;
            this.aiJsonAddAiBtn.Text = "Add to Level";
            this.aiJsonAddAiBtn.UseVisualStyleBackColor = true;
            this.aiJsonAddAiBtn.Click += new System.EventHandler(this.aiJsonAddAiBtn_Click);
            // 
            // aiFileNameTxt
            // 
            this.aiFileNameTxt.BackColor = System.Drawing.SystemColors.Control;
            this.aiFileNameTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.aiFileNameTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.aiFileNameTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.aiFileNameTxt.Location = new System.Drawing.Point(881, 36);
            this.aiFileNameTxt.Margin = new System.Windows.Forms.Padding(4);
            this.aiFileNameTxt.Name = "aiFileNameTxt";
            this.aiFileNameTxt.ReadOnly = true;
            this.aiFileNameTxt.Size = new System.Drawing.Size(223, 30);
            this.aiFileNameTxt.TabIndex = 57;
            // 
            // aiFileSizeTxt
            // 
            this.aiFileSizeTxt.BackColor = System.Drawing.SystemColors.Control;
            this.aiFileSizeTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.aiFileSizeTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.aiFileSizeTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.aiFileSizeTxt.Location = new System.Drawing.Point(881, 74);
            this.aiFileSizeTxt.Margin = new System.Windows.Forms.Padding(4);
            this.aiFileSizeTxt.Name = "aiFileSizeTxt";
            this.aiFileSizeTxt.ReadOnly = true;
            this.aiFileSizeTxt.Size = new System.Drawing.Size(223, 30);
            this.aiFileSizeTxt.TabIndex = 58;
            // 
            // aiJsonVersionTxt
            // 
            this.aiJsonVersionTxt.BackColor = System.Drawing.SystemColors.Control;
            this.aiJsonVersionTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.aiJsonVersionTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.aiJsonVersionTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.aiJsonVersionTxt.Location = new System.Drawing.Point(881, -2);
            this.aiJsonVersionTxt.Margin = new System.Windows.Forms.Padding(4);
            this.aiJsonVersionTxt.Name = "aiJsonVersionTxt";
            this.aiJsonVersionTxt.Size = new System.Drawing.Size(223, 30);
            this.aiJsonVersionTxt.TabIndex = 59;
            this.aiJsonVersionTxt.Text = "JSON v1.1";
            // 
            // aiJsonSaveAiBtn
            // 
            this.aiJsonSaveAiBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.aiJsonSaveAiBtn.Font = new System.Drawing.Font("Century Gothic", 10.2F);
            this.aiJsonSaveAiBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.aiJsonSaveAiBtn.Location = new System.Drawing.Point(871, 311);
            this.aiJsonSaveAiBtn.Margin = new System.Windows.Forms.Padding(4);
            this.aiJsonSaveAiBtn.Name = "aiJsonSaveAiBtn";
            this.aiJsonSaveAiBtn.Size = new System.Drawing.Size(254, 41);
            this.aiJsonSaveAiBtn.TabIndex = 56;
            this.aiJsonSaveAiBtn.Text = "Save to List";
            this.aiJsonSaveAiBtn.UseVisualStyleBackColor = true;
            this.aiJsonSaveAiBtn.Click += new System.EventHandler(this.aiJsonSaveAiBtn_Click);
            // 
            // aiScriptEditor
            // 
            this.aiScriptEditor.BackColor = System.Drawing.SystemColors.Control;
            this.aiScriptEditor.Location = new System.Drawing.Point(4, 26);
            this.aiScriptEditor.Name = "aiScriptEditor";
            this.aiScriptEditor.Padding = new System.Windows.Forms.Padding(3);
            this.aiScriptEditor.Size = new System.Drawing.Size(1129, 378);
            this.aiScriptEditor.TabIndex = 2;
            this.aiScriptEditor.Text = "Script Editor";
            // 
            // aiPatrolPathEditor
            // 
            this.aiPatrolPathEditor.BackColor = System.Drawing.SystemColors.Control;
            this.aiPatrolPathEditor.Location = new System.Drawing.Point(4, 26);
            this.aiPatrolPathEditor.Name = "aiPatrolPathEditor";
            this.aiPatrolPathEditor.Size = new System.Drawing.Size(1129, 378);
            this.aiPatrolPathEditor.TabIndex = 3;
            this.aiPatrolPathEditor.Text = "Patrol Editor";
            // 
            // weaponEditor
            // 
            this.weaponEditor.BackColor = System.Drawing.SystemColors.Control;
            this.weaponEditor.Controls.Add(this.weaponEditorTabs);
            this.weaponEditor.ForeColor = System.Drawing.SystemColors.ControlText;
            this.weaponEditor.Location = new System.Drawing.Point(4, 30);
            this.weaponEditor.Margin = new System.Windows.Forms.Padding(4);
            this.weaponEditor.Name = "weaponEditor";
            this.weaponEditor.Size = new System.Drawing.Size(1137, 387);
            this.weaponEditor.TabIndex = 7;
            this.weaponEditor.Text = "Weapon Editor";
            // 
            // weaponEditorTabs
            // 
            this.weaponEditorTabs.Controls.Add(this.weaponEditorMainTab);
            this.weaponEditorTabs.Controls.Add(this.weaponCfgEditor);
            this.weaponEditorTabs.Controls.Add(this.weaponCfgAdvanceEditor);
            this.weaponEditorTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.weaponEditorTabs.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.weaponEditorTabs.Location = new System.Drawing.Point(0, 0);
            this.weaponEditorTabs.Margin = new System.Windows.Forms.Padding(4);
            this.weaponEditorTabs.Multiline = true;
            this.weaponEditorTabs.Name = "weaponEditorTabs";
            this.weaponEditorTabs.SelectedIndex = 0;
            this.weaponEditorTabs.Size = new System.Drawing.Size(1137, 387);
            this.weaponEditorTabs.TabIndex = 86;
            // 
            // weaponEditorMainTab
            // 
            this.weaponEditorMainTab.BackColor = System.Drawing.SystemColors.Control;
            this.weaponEditorMainTab.Controls.Add(this.currentWeaponCb);
            this.weaponEditorMainTab.Controls.Add(this.weaponGroupFileTxt);
            this.weaponEditorMainTab.Controls.Add(this.loadWeaponGroupBtn);
            this.weaponEditorMainTab.Controls.Add(this.saveWeaponGroupBtn);
            this.weaponEditorMainTab.Controls.Add(this.weaponAmmoTxt);
            this.weaponEditorMainTab.Controls.Add(this.markWeaponsCb);
            this.weaponEditorMainTab.Controls.Add(this.allWeaponsCb);
            this.weaponEditorMainTab.Controls.Add(this.label4);
            this.weaponEditorMainTab.Controls.Add(this.weaponDD);
            this.weaponEditorMainTab.Controls.Add(this.weaponImgBox);
            this.weaponEditorMainTab.Controls.Add(this.removeWeaponBtn);
            this.weaponEditorMainTab.Controls.Add(this.label5);
            this.weaponEditorMainTab.Controls.Add(this.addWeaponBtn);
            this.weaponEditorMainTab.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.weaponEditorMainTab.ForeColor = System.Drawing.SystemColors.ControlText;
            this.weaponEditorMainTab.Location = new System.Drawing.Point(4, 26);
            this.weaponEditorMainTab.Margin = new System.Windows.Forms.Padding(4);
            this.weaponEditorMainTab.Name = "weaponEditorMainTab";
            this.weaponEditorMainTab.Size = new System.Drawing.Size(1129, 357);
            this.weaponEditorMainTab.TabIndex = 0;
            this.weaponEditorMainTab.Text = "Main Editor";
            this.weaponEditorMainTab.Click += new System.EventHandler(this.weaponEditorMainTab_Click);
            // 
            // currentWeaponCb
            // 
            this.currentWeaponCb.AutoSize = true;
            this.currentWeaponCb.Font = new System.Drawing.Font("Century Gothic", 7.8F);
            this.currentWeaponCb.Location = new System.Drawing.Point(966, 329);
            this.currentWeaponCb.Margin = new System.Windows.Forms.Padding(4);
            this.currentWeaponCb.Name = "currentWeaponCb";
            this.currentWeaponCb.Size = new System.Drawing.Size(148, 23);
            this.currentWeaponCb.TabIndex = 156;
            this.currentWeaponCb.Text = "Current Weapon";
            this.currentWeaponCb.UseVisualStyleBackColor = true;
            this.currentWeaponCb.CheckedChanged += new System.EventHandler(this.currentWeaponCb_CheckedChanged);
            // 
            // weaponGroupFileTxt
            // 
            this.weaponGroupFileTxt.BackColor = System.Drawing.SystemColors.Control;
            this.weaponGroupFileTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.weaponGroupFileTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.weaponGroupFileTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.weaponGroupFileTxt.Location = new System.Drawing.Point(255, 148);
            this.weaponGroupFileTxt.Margin = new System.Windows.Forms.Padding(4);
            this.weaponGroupFileTxt.Name = "weaponGroupFileTxt";
            this.weaponGroupFileTxt.Size = new System.Drawing.Size(401, 30);
            this.weaponGroupFileTxt.TabIndex = 122;
            this.weaponGroupFileTxt.Text = "WEAPON GROUP NAME";
            this.infoViewer.SetToolTip(this.weaponGroupFileTxt, "Weapon Group File Name");
            // 
            // loadWeaponGroupBtn
            // 
            this.loadWeaponGroupBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loadWeaponGroupBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.loadWeaponGroupBtn.Location = new System.Drawing.Point(894, 148);
            this.loadWeaponGroupBtn.Margin = new System.Windows.Forms.Padding(4);
            this.loadWeaponGroupBtn.Name = "loadWeaponGroupBtn";
            this.loadWeaponGroupBtn.Size = new System.Drawing.Size(211, 31);
            this.loadWeaponGroupBtn.TabIndex = 121;
            this.loadWeaponGroupBtn.Text = "Load Group";
            this.loadWeaponGroupBtn.UseVisualStyleBackColor = true;
            this.loadWeaponGroupBtn.Click += new System.EventHandler(this.loadWeaponGroupBtn_Click);
            // 
            // saveWeaponGroupBtn
            // 
            this.saveWeaponGroupBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveWeaponGroupBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.saveWeaponGroupBtn.Location = new System.Drawing.Point(675, 148);
            this.saveWeaponGroupBtn.Margin = new System.Windows.Forms.Padding(4);
            this.saveWeaponGroupBtn.Name = "saveWeaponGroupBtn";
            this.saveWeaponGroupBtn.Size = new System.Drawing.Size(211, 31);
            this.saveWeaponGroupBtn.TabIndex = 120;
            this.saveWeaponGroupBtn.Text = "Save Group";
            this.saveWeaponGroupBtn.UseVisualStyleBackColor = true;
            this.saveWeaponGroupBtn.Click += new System.EventHandler(this.saveWeaponGroupBtn_Click);
            // 
            // weaponAmmoTxt
            // 
            this.weaponAmmoTxt.BackColor = System.Drawing.SystemColors.Control;
            this.weaponAmmoTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.weaponAmmoTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.weaponAmmoTxt.Location = new System.Drawing.Point(237, 67);
            this.weaponAmmoTxt.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.weaponAmmoTxt.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.weaponAmmoTxt.Name = "weaponAmmoTxt";
            this.weaponAmmoTxt.Size = new System.Drawing.Size(101, 30);
            this.weaponAmmoTxt.TabIndex = 115;
            this.weaponAmmoTxt.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // markWeaponsCb
            // 
            this.markWeaponsCb.AutoSize = true;
            this.markWeaponsCb.Font = new System.Drawing.Font("Century Gothic", 7.8F);
            this.markWeaponsCb.Location = new System.Drawing.Point(966, 308);
            this.markWeaponsCb.Margin = new System.Windows.Forms.Padding(4);
            this.markWeaponsCb.Name = "markWeaponsCb";
            this.markWeaponsCb.Size = new System.Drawing.Size(131, 23);
            this.markWeaponsCb.TabIndex = 113;
            this.markWeaponsCb.Text = "Mark Weapon";
            this.markWeaponsCb.UseVisualStyleBackColor = true;
            // 
            // allWeaponsCb
            // 
            this.allWeaponsCb.AutoSize = true;
            this.allWeaponsCb.Font = new System.Drawing.Font("Century Gothic", 7.8F);
            this.allWeaponsCb.Location = new System.Drawing.Point(966, 284);
            this.allWeaponsCb.Margin = new System.Windows.Forms.Padding(4);
            this.allWeaponsCb.Name = "allWeaponsCb";
            this.allWeaponsCb.Size = new System.Drawing.Size(117, 23);
            this.allWeaponsCb.TabIndex = 114;
            this.allWeaponsCb.Text = "All Weapons";
            this.allWeaponsCb.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(49, 64);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(173, 33);
            this.label4.TabIndex = 112;
            this.label4.Text = "Ammo ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // weaponDD
            // 
            this.weaponDD.BackColor = System.Drawing.SystemColors.Control;
            this.weaponDD.Cursor = System.Windows.Forms.Cursors.Hand;
            this.weaponDD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.weaponDD.ForeColor = System.Drawing.SystemColors.WindowText;
            this.weaponDD.FormattingEnabled = true;
            this.weaponDD.Location = new System.Drawing.Point(237, 12);
            this.weaponDD.Name = "weaponDD";
            this.weaponDD.Size = new System.Drawing.Size(419, 29);
            this.weaponDD.TabIndex = 111;
            this.weaponDD.SelectedValueChanged += new System.EventHandler(this.weaponSelectDD_SelectedValueChanged);
            // 
            // weaponImgBox
            // 
            this.weaponImgBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.weaponImgBox.Location = new System.Drawing.Point(23, 123);
            this.weaponImgBox.Margin = new System.Windows.Forms.Padding(4);
            this.weaponImgBox.Name = "weaponImgBox";
            this.weaponImgBox.Size = new System.Drawing.Size(199, 184);
            this.weaponImgBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.weaponImgBox.TabIndex = 110;
            this.weaponImgBox.TabStop = false;
            // 
            // removeWeaponBtn
            // 
            this.removeWeaponBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.removeWeaponBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.removeWeaponBtn.Location = new System.Drawing.Point(894, 10);
            this.removeWeaponBtn.Margin = new System.Windows.Forms.Padding(4);
            this.removeWeaponBtn.Name = "removeWeaponBtn";
            this.removeWeaponBtn.Size = new System.Drawing.Size(211, 31);
            this.removeWeaponBtn.TabIndex = 109;
            this.removeWeaponBtn.Text = "Remove Weapon";
            this.removeWeaponBtn.UseVisualStyleBackColor = true;
            this.removeWeaponBtn.Click += new System.EventHandler(this.removeWeaponBtn_Click);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(46, 8);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(173, 33);
            this.label5.TabIndex = 106;
            this.label5.Text = "Weapon";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // addWeaponBtn
            // 
            this.addWeaponBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addWeaponBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.addWeaponBtn.Location = new System.Drawing.Point(675, 10);
            this.addWeaponBtn.Margin = new System.Windows.Forms.Padding(4);
            this.addWeaponBtn.Name = "addWeaponBtn";
            this.addWeaponBtn.Size = new System.Drawing.Size(211, 31);
            this.addWeaponBtn.TabIndex = 105;
            this.addWeaponBtn.Text = "Add Weapon";
            this.addWeaponBtn.UseVisualStyleBackColor = true;
            this.addWeaponBtn.Click += new System.EventHandler(this.addWeaponBtn_Click);
            // 
            // weaponCfgEditor
            // 
            this.weaponCfgEditor.BackColor = System.Drawing.SystemColors.Control;
            this.weaponCfgEditor.Controls.Add(this.resetWeaponBtn);
            this.weaponCfgEditor.Controls.Add(this.weaponRangeTxt);
            this.weaponCfgEditor.Controls.Add(this.weaponRangeLbl);
            this.weaponCfgEditor.Controls.Add(this.weaponRoundPerMinuteTxt);
            this.weaponCfgEditor.Controls.Add(this.weaponBulletsTxt);
            this.weaponCfgEditor.Controls.Add(this.weaponPowerTxt);
            this.weaponCfgEditor.Controls.Add(this.weaponDamageTxt);
            this.weaponCfgEditor.Controls.Add(this.weaponRoundPerClipTxt);
            this.weaponCfgEditor.Controls.Add(this.weaponRoundPerClipLbl);
            this.weaponCfgEditor.Controls.Add(this.weaponRoundPerMinuteLbl);
            this.weaponCfgEditor.Controls.Add(this.updateWeaponPowerDamageBtn);
            this.weaponCfgEditor.Controls.Add(this.weaponBulletsLbl);
            this.weaponCfgEditor.Controls.Add(this.weaponPowerLbl);
            this.weaponCfgEditor.Controls.Add(this.weaponDamageLbl);
            this.weaponCfgEditor.Controls.Add(this.weaponSfx2DD);
            this.weaponCfgEditor.Controls.Add(this.weaponSfx1DD);
            this.weaponCfgEditor.Controls.Add(this.weaponDisplayTypeDD);
            this.weaponCfgEditor.Controls.Add(this.sfx2Lbl);
            this.weaponCfgEditor.Controls.Add(this.weaponSightTypeDD);
            this.weaponCfgEditor.Controls.Add(this.sfx1Lbl);
            this.weaponCfgEditor.Controls.Add(this.label6);
            this.weaponCfgEditor.Controls.Add(this.updateWeaponSFXBtn);
            this.weaponCfgEditor.Controls.Add(this.label7);
            this.weaponCfgEditor.Controls.Add(this.updateWeaponUIBtn);
            this.weaponCfgEditor.Controls.Add(this.label2);
            this.weaponCfgEditor.Controls.Add(this.weaponDescriptionTxt);
            this.weaponCfgEditor.Controls.Add(this.weaponNameLbl);
            this.weaponCfgEditor.Controls.Add(this.weaponCfgDD);
            this.weaponCfgEditor.Controls.Add(this.weaponTypeLblDD);
            this.weaponCfgEditor.Controls.Add(this.updateWeaponDetailsBtn);
            this.weaponCfgEditor.Controls.Add(this.weaponNameTxt);
            this.weaponCfgEditor.Controls.Add(this.updateWeaponPropertiesBtn);
            this.weaponCfgEditor.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.weaponCfgEditor.ForeColor = System.Drawing.SystemColors.ControlText;
            this.weaponCfgEditor.Location = new System.Drawing.Point(4, 26);
            this.weaponCfgEditor.Name = "weaponCfgEditor";
            this.weaponCfgEditor.Padding = new System.Windows.Forms.Padding(3);
            this.weaponCfgEditor.Size = new System.Drawing.Size(1129, 357);
            this.weaponCfgEditor.TabIndex = 1;
            this.weaponCfgEditor.Text = "Weapon Editor";
            this.weaponCfgEditor.Click += new System.EventHandler(this.weaponCfgEditor_Click);
            // 
            // resetWeaponBtn
            // 
            this.resetWeaponBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resetWeaponBtn.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.resetWeaponBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.resetWeaponBtn.Location = new System.Drawing.Point(949, 314);
            this.resetWeaponBtn.Margin = new System.Windows.Forms.Padding(4);
            this.resetWeaponBtn.Name = "resetWeaponBtn";
            this.resetWeaponBtn.Size = new System.Drawing.Size(159, 31);
            this.resetWeaponBtn.TabIndex = 155;
            this.resetWeaponBtn.Text = "Reset";
            this.resetWeaponBtn.UseVisualStyleBackColor = true;
            this.resetWeaponBtn.Click += new System.EventHandler(this.resetWeaponBtn_Click);
            // 
            // weaponRangeTxt
            // 
            this.weaponRangeTxt.BackColor = System.Drawing.SystemColors.Control;
            this.weaponRangeTxt.DecimalPlaces = 2;
            this.weaponRangeTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.weaponRangeTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.weaponRangeTxt.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.weaponRangeTxt.Location = new System.Drawing.Point(676, 313);
            this.weaponRangeTxt.Maximum = new decimal(new int[] {
            -805306368,
            967903254,
            1843143693,
            0});
            this.weaponRangeTxt.Name = "weaponRangeTxt";
            this.weaponRangeTxt.Size = new System.Drawing.Size(100, 30);
            this.weaponRangeTxt.TabIndex = 153;
            this.weaponRangeTxt.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // weaponRangeLbl
            // 
            this.weaponRangeLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.weaponRangeLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.weaponRangeLbl.Location = new System.Drawing.Point(585, 310);
            this.weaponRangeLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.weaponRangeLbl.Name = "weaponRangeLbl";
            this.weaponRangeLbl.Size = new System.Drawing.Size(84, 33);
            this.weaponRangeLbl.TabIndex = 152;
            this.weaponRangeLbl.Text = "Range";
            this.weaponRangeLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // weaponRoundPerMinuteTxt
            // 
            this.weaponRoundPerMinuteTxt.BackColor = System.Drawing.SystemColors.Control;
            this.weaponRoundPerMinuteTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.weaponRoundPerMinuteTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.weaponRoundPerMinuteTxt.Location = new System.Drawing.Point(184, 310);
            this.weaponRoundPerMinuteTxt.Maximum = new decimal(new int[] {
            -805306368,
            967903254,
            1843143693,
            0});
            this.weaponRoundPerMinuteTxt.Name = "weaponRoundPerMinuteTxt";
            this.weaponRoundPerMinuteTxt.Size = new System.Drawing.Size(100, 30);
            this.weaponRoundPerMinuteTxt.TabIndex = 151;
            this.weaponRoundPerMinuteTxt.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // weaponBulletsTxt
            // 
            this.weaponBulletsTxt.BackColor = System.Drawing.SystemColors.Control;
            this.weaponBulletsTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.weaponBulletsTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.weaponBulletsTxt.Location = new System.Drawing.Point(676, 250);
            this.weaponBulletsTxt.Maximum = new decimal(new int[] {
            -805306368,
            967903254,
            1843143693,
            0});
            this.weaponBulletsTxt.Name = "weaponBulletsTxt";
            this.weaponBulletsTxt.Size = new System.Drawing.Size(100, 30);
            this.weaponBulletsTxt.TabIndex = 150;
            this.weaponBulletsTxt.Value = new decimal(new int[] {
            70,
            0,
            0,
            0});
            // 
            // weaponPowerTxt
            // 
            this.weaponPowerTxt.BackColor = System.Drawing.SystemColors.Control;
            this.weaponPowerTxt.DecimalPlaces = 2;
            this.weaponPowerTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.weaponPowerTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.weaponPowerTxt.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.weaponPowerTxt.Location = new System.Drawing.Point(453, 250);
            this.weaponPowerTxt.Maximum = new decimal(new int[] {
            -805306368,
            967903254,
            1843143693,
            0});
            this.weaponPowerTxt.Name = "weaponPowerTxt";
            this.weaponPowerTxt.Size = new System.Drawing.Size(100, 30);
            this.weaponPowerTxt.TabIndex = 149;
            this.weaponPowerTxt.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // weaponDamageTxt
            // 
            this.weaponDamageTxt.BackColor = System.Drawing.SystemColors.Control;
            this.weaponDamageTxt.DecimalPlaces = 2;
            this.weaponDamageTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.weaponDamageTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.weaponDamageTxt.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.weaponDamageTxt.Location = new System.Drawing.Point(184, 250);
            this.weaponDamageTxt.Maximum = new decimal(new int[] {
            -805306368,
            967903254,
            1843143693,
            0});
            this.weaponDamageTxt.Name = "weaponDamageTxt";
            this.weaponDamageTxt.Size = new System.Drawing.Size(100, 30);
            this.weaponDamageTxt.TabIndex = 148;
            this.weaponDamageTxt.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // weaponRoundPerClipTxt
            // 
            this.weaponRoundPerClipTxt.BackColor = System.Drawing.SystemColors.Control;
            this.weaponRoundPerClipTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.weaponRoundPerClipTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.weaponRoundPerClipTxt.Location = new System.Drawing.Point(453, 314);
            this.weaponRoundPerClipTxt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.weaponRoundPerClipTxt.Maximum = new decimal(new int[] {
            -805306368,
            967903254,
            1843143693,
            0});
            this.weaponRoundPerClipTxt.Name = "weaponRoundPerClipTxt";
            this.weaponRoundPerClipTxt.Size = new System.Drawing.Size(101, 30);
            this.weaponRoundPerClipTxt.TabIndex = 147;
            this.weaponRoundPerClipTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.weaponRoundPerClipTxt.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // weaponRoundPerClipLbl
            // 
            this.weaponRoundPerClipLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.weaponRoundPerClipLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.weaponRoundPerClipLbl.Location = new System.Drawing.Point(305, 313);
            this.weaponRoundPerClipLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.weaponRoundPerClipLbl.Name = "weaponRoundPerClipLbl";
            this.weaponRoundPerClipLbl.Size = new System.Drawing.Size(131, 33);
            this.weaponRoundPerClipLbl.TabIndex = 154;
            this.weaponRoundPerClipLbl.Text = "Rounds/Clip";
            this.weaponRoundPerClipLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // weaponRoundPerMinuteLbl
            // 
            this.weaponRoundPerMinuteLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.weaponRoundPerMinuteLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.weaponRoundPerMinuteLbl.Location = new System.Drawing.Point(7, 310);
            this.weaponRoundPerMinuteLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.weaponRoundPerMinuteLbl.Name = "weaponRoundPerMinuteLbl";
            this.weaponRoundPerMinuteLbl.Size = new System.Drawing.Size(164, 33);
            this.weaponRoundPerMinuteLbl.TabIndex = 147;
            this.weaponRoundPerMinuteLbl.Text = "Rounds/Minute";
            this.weaponRoundPerMinuteLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // updateWeaponPowerDamageBtn
            // 
            this.updateWeaponPowerDamageBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.updateWeaponPowerDamageBtn.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.updateWeaponPowerDamageBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.updateWeaponPowerDamageBtn.Location = new System.Drawing.Point(784, 254);
            this.updateWeaponPowerDamageBtn.Margin = new System.Windows.Forms.Padding(4);
            this.updateWeaponPowerDamageBtn.Name = "updateWeaponPowerDamageBtn";
            this.updateWeaponPowerDamageBtn.Size = new System.Drawing.Size(324, 31);
            this.updateWeaponPowerDamageBtn.TabIndex = 146;
            this.updateWeaponPowerDamageBtn.Text = "Update Power";
            this.updateWeaponPowerDamageBtn.UseVisualStyleBackColor = true;
            this.updateWeaponPowerDamageBtn.Click += new System.EventHandler(this.updateWeaponPowerDamageBtn_Click);
            // 
            // weaponBulletsLbl
            // 
            this.weaponBulletsLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.weaponBulletsLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.weaponBulletsLbl.Location = new System.Drawing.Point(579, 250);
            this.weaponBulletsLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.weaponBulletsLbl.Name = "weaponBulletsLbl";
            this.weaponBulletsLbl.Size = new System.Drawing.Size(90, 33);
            this.weaponBulletsLbl.TabIndex = 145;
            this.weaponBulletsLbl.Text = "Bullets";
            this.weaponBulletsLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // weaponPowerLbl
            // 
            this.weaponPowerLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.weaponPowerLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.weaponPowerLbl.Location = new System.Drawing.Point(322, 247);
            this.weaponPowerLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.weaponPowerLbl.Name = "weaponPowerLbl";
            this.weaponPowerLbl.Size = new System.Drawing.Size(84, 33);
            this.weaponPowerLbl.TabIndex = 144;
            this.weaponPowerLbl.Text = "Power";
            this.weaponPowerLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // weaponDamageLbl
            // 
            this.weaponDamageLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.weaponDamageLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.weaponDamageLbl.Location = new System.Drawing.Point(10, 250);
            this.weaponDamageLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.weaponDamageLbl.Name = "weaponDamageLbl";
            this.weaponDamageLbl.Size = new System.Drawing.Size(123, 33);
            this.weaponDamageLbl.TabIndex = 143;
            this.weaponDamageLbl.Text = "Damage";
            this.weaponDamageLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // weaponSfx2DD
            // 
            this.weaponSfx2DD.BackColor = System.Drawing.SystemColors.Control;
            this.weaponSfx2DD.Cursor = System.Windows.Forms.Cursors.Hand;
            this.weaponSfx2DD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.weaponSfx2DD.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.weaponSfx2DD.ForeColor = System.Drawing.SystemColors.WindowText;
            this.weaponSfx2DD.FormattingEnabled = true;
            this.weaponSfx2DD.Location = new System.Drawing.Point(560, 190);
            this.weaponSfx2DD.Name = "weaponSfx2DD";
            this.weaponSfx2DD.Size = new System.Drawing.Size(208, 29);
            this.weaponSfx2DD.TabIndex = 142;
            // 
            // weaponSfx1DD
            // 
            this.weaponSfx1DD.BackColor = System.Drawing.SystemColors.Control;
            this.weaponSfx1DD.Cursor = System.Windows.Forms.Cursors.Hand;
            this.weaponSfx1DD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.weaponSfx1DD.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.weaponSfx1DD.ForeColor = System.Drawing.SystemColors.WindowText;
            this.weaponSfx1DD.FormattingEnabled = true;
            this.weaponSfx1DD.Location = new System.Drawing.Point(140, 190);
            this.weaponSfx1DD.Name = "weaponSfx1DD";
            this.weaponSfx1DD.Size = new System.Drawing.Size(208, 29);
            this.weaponSfx1DD.TabIndex = 141;
            // 
            // weaponDisplayTypeDD
            // 
            this.weaponDisplayTypeDD.BackColor = System.Drawing.SystemColors.Control;
            this.weaponDisplayTypeDD.Cursor = System.Windows.Forms.Cursors.Hand;
            this.weaponDisplayTypeDD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.weaponDisplayTypeDD.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.weaponDisplayTypeDD.ForeColor = System.Drawing.SystemColors.WindowText;
            this.weaponDisplayTypeDD.FormattingEnabled = true;
            this.weaponDisplayTypeDD.Items.AddRange(new object[] {
            "GRENADE",
            "SHELL",
            "BARREL",
            "CLIP",
            "NORMAL",
            "NONE"});
            this.weaponDisplayTypeDD.Location = new System.Drawing.Point(560, 124);
            this.weaponDisplayTypeDD.Name = "weaponDisplayTypeDD";
            this.weaponDisplayTypeDD.Size = new System.Drawing.Size(208, 29);
            this.weaponDisplayTypeDD.TabIndex = 142;
            // 
            // sfx2Lbl
            // 
            this.sfx2Lbl.Font = new System.Drawing.Font("Century Gothic", 10.2F);
            this.sfx2Lbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.sfx2Lbl.Location = new System.Drawing.Point(407, 190);
            this.sfx2Lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.sfx2Lbl.Name = "sfx2Lbl";
            this.sfx2Lbl.Size = new System.Drawing.Size(146, 33);
            this.sfx2Lbl.TabIndex = 140;
            this.sfx2Lbl.Text = "Sound Effect 2";
            this.sfx2Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // weaponSightTypeDD
            // 
            this.weaponSightTypeDD.BackColor = System.Drawing.SystemColors.Control;
            this.weaponSightTypeDD.Cursor = System.Windows.Forms.Cursors.Hand;
            this.weaponSightTypeDD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.weaponSightTypeDD.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.weaponSightTypeDD.ForeColor = System.Drawing.SystemColors.WindowText;
            this.weaponSightTypeDD.FormattingEnabled = true;
            this.weaponSightTypeDD.Items.AddRange(new object[] {
            "CROSSHAIR",
            "NONE"});
            this.weaponSightTypeDD.Location = new System.Drawing.Point(140, 124);
            this.weaponSightTypeDD.Name = "weaponSightTypeDD";
            this.weaponSightTypeDD.Size = new System.Drawing.Size(208, 29);
            this.weaponSightTypeDD.TabIndex = 141;
            // 
            // sfx1Lbl
            // 
            this.sfx1Lbl.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sfx1Lbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.sfx1Lbl.Location = new System.Drawing.Point(7, 186);
            this.sfx1Lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.sfx1Lbl.Name = "sfx1Lbl";
            this.sfx1Lbl.Size = new System.Drawing.Size(133, 33);
            this.sfx1Lbl.TabIndex = 138;
            this.sfx1Lbl.Text = "Sound Effect 1";
            this.sfx1Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(407, 124);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(146, 33);
            this.label6.TabIndex = 140;
            this.label6.Text = "Display Type";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // updateWeaponSFXBtn
            // 
            this.updateWeaponSFXBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.updateWeaponSFXBtn.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.updateWeaponSFXBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.updateWeaponSFXBtn.Location = new System.Drawing.Point(784, 190);
            this.updateWeaponSFXBtn.Margin = new System.Windows.Forms.Padding(4);
            this.updateWeaponSFXBtn.Name = "updateWeaponSFXBtn";
            this.updateWeaponSFXBtn.Size = new System.Drawing.Size(323, 31);
            this.updateWeaponSFXBtn.TabIndex = 137;
            this.updateWeaponSFXBtn.Text = "Update SFX";
            this.updateWeaponSFXBtn.UseVisualStyleBackColor = true;
            this.updateWeaponSFXBtn.Click += new System.EventHandler(this.updateWeaponSFXBtn_Click);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(19, 120);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(114, 33);
            this.label7.TabIndex = 138;
            this.label7.Text = "Sight Type";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // updateWeaponUIBtn
            // 
            this.updateWeaponUIBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.updateWeaponUIBtn.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.updateWeaponUIBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.updateWeaponUIBtn.Location = new System.Drawing.Point(784, 124);
            this.updateWeaponUIBtn.Margin = new System.Windows.Forms.Padding(4);
            this.updateWeaponUIBtn.Name = "updateWeaponUIBtn";
            this.updateWeaponUIBtn.Size = new System.Drawing.Size(323, 31);
            this.updateWeaponUIBtn.TabIndex = 137;
            this.updateWeaponUIBtn.Text = "Update UI";
            this.updateWeaponUIBtn.UseVisualStyleBackColor = true;
            this.updateWeaponUIBtn.Click += new System.EventHandler(this.updateWeaponUIBtn_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(417, 69);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 33);
            this.label2.TabIndex = 135;
            this.label2.Text = "Description";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // weaponDescriptionTxt
            // 
            this.weaponDescriptionTxt.BackColor = System.Drawing.SystemColors.Control;
            this.weaponDescriptionTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.weaponDescriptionTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.weaponDescriptionTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.weaponDescriptionTxt.Location = new System.Drawing.Point(562, 70);
            this.weaponDescriptionTxt.Margin = new System.Windows.Forms.Padding(4);
            this.weaponDescriptionTxt.Name = "weaponDescriptionTxt";
            this.weaponDescriptionTxt.Size = new System.Drawing.Size(208, 30);
            this.weaponDescriptionTxt.TabIndex = 134;
            this.weaponDescriptionTxt.Text = "<SNIPDESC>";
            this.infoViewer.SetToolTip(this.weaponDescriptionTxt, "Weapon Description");
            // 
            // weaponNameLbl
            // 
            this.weaponNameLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.weaponNameLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.weaponNameLbl.Location = new System.Drawing.Point(19, 70);
            this.weaponNameLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.weaponNameLbl.Name = "weaponNameLbl";
            this.weaponNameLbl.Size = new System.Drawing.Size(114, 33);
            this.weaponNameLbl.TabIndex = 133;
            this.weaponNameLbl.Text = "Name";
            this.weaponNameLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // weaponCfgDD
            // 
            this.weaponCfgDD.BackColor = System.Drawing.SystemColors.Control;
            this.weaponCfgDD.Cursor = System.Windows.Forms.Cursors.Hand;
            this.weaponCfgDD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.weaponCfgDD.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.weaponCfgDD.ForeColor = System.Drawing.SystemColors.WindowText;
            this.weaponCfgDD.FormattingEnabled = true;
            this.weaponCfgDD.Location = new System.Drawing.Point(316, 6);
            this.weaponCfgDD.Name = "weaponCfgDD";
            this.weaponCfgDD.Size = new System.Drawing.Size(419, 29);
            this.weaponCfgDD.TabIndex = 123;
            this.weaponCfgDD.SelectedIndexChanged += new System.EventHandler(this.weaponCfgDD_SelectedIndexChanged);
            // 
            // weaponTypeLblDD
            // 
            this.weaponTypeLblDD.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.weaponTypeLblDD.ForeColor = System.Drawing.SystemColors.ControlText;
            this.weaponTypeLblDD.Location = new System.Drawing.Point(125, 2);
            this.weaponTypeLblDD.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.weaponTypeLblDD.Name = "weaponTypeLblDD";
            this.weaponTypeLblDD.Size = new System.Drawing.Size(173, 33);
            this.weaponTypeLblDD.TabIndex = 122;
            this.weaponTypeLblDD.Text = "Weapon";
            this.weaponTypeLblDD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // updateWeaponDetailsBtn
            // 
            this.updateWeaponDetailsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.updateWeaponDetailsBtn.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.updateWeaponDetailsBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.updateWeaponDetailsBtn.Location = new System.Drawing.Point(784, 70);
            this.updateWeaponDetailsBtn.Margin = new System.Windows.Forms.Padding(4);
            this.updateWeaponDetailsBtn.Name = "updateWeaponDetailsBtn";
            this.updateWeaponDetailsBtn.Size = new System.Drawing.Size(324, 31);
            this.updateWeaponDetailsBtn.TabIndex = 121;
            this.updateWeaponDetailsBtn.Text = "Update Details";
            this.updateWeaponDetailsBtn.UseVisualStyleBackColor = true;
            this.updateWeaponDetailsBtn.Click += new System.EventHandler(this.updateWeaponDetailsBtn_Click);
            // 
            // weaponNameTxt
            // 
            this.weaponNameTxt.BackColor = System.Drawing.SystemColors.Control;
            this.weaponNameTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.weaponNameTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.weaponNameTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.weaponNameTxt.Location = new System.Drawing.Point(141, 70);
            this.weaponNameTxt.Margin = new System.Windows.Forms.Padding(4);
            this.weaponNameTxt.Name = "weaponNameTxt";
            this.weaponNameTxt.Size = new System.Drawing.Size(208, 30);
            this.weaponNameTxt.TabIndex = 120;
            this.weaponNameTxt.Text = "Sniperz";
            this.infoViewer.SetToolTip(this.weaponNameTxt, "Weapon Name");
            // 
            // updateWeaponPropertiesBtn
            // 
            this.updateWeaponPropertiesBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.updateWeaponPropertiesBtn.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.updateWeaponPropertiesBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.updateWeaponPropertiesBtn.Location = new System.Drawing.Point(783, 314);
            this.updateWeaponPropertiesBtn.Margin = new System.Windows.Forms.Padding(4);
            this.updateWeaponPropertiesBtn.Name = "updateWeaponPropertiesBtn";
            this.updateWeaponPropertiesBtn.Size = new System.Drawing.Size(166, 31);
            this.updateWeaponPropertiesBtn.TabIndex = 119;
            this.updateWeaponPropertiesBtn.Text = "Update";
            this.updateWeaponPropertiesBtn.UseVisualStyleBackColor = true;
            this.updateWeaponPropertiesBtn.Click += new System.EventHandler(this.updateWeaponPropertiesBtn_Click);
            // 
            // weaponCfgAdvanceEditor
            // 
            this.weaponCfgAdvanceEditor.BackColor = System.Drawing.SystemColors.Control;
            this.weaponCfgAdvanceEditor.Location = new System.Drawing.Point(4, 26);
            this.weaponCfgAdvanceEditor.Name = "weaponCfgAdvanceEditor";
            this.weaponCfgAdvanceEditor.Padding = new System.Windows.Forms.Padding(3);
            this.weaponCfgAdvanceEditor.Size = new System.Drawing.Size(1129, 357);
            this.weaponCfgAdvanceEditor.TabIndex = 2;
            this.weaponCfgAdvanceEditor.Text = "Advance Editor";
            // 
            // threeDEditor
            // 
            this.threeDEditor.BackColor = System.Drawing.SystemColors.Control;
            this.threeDEditor.Controls.Add(this.resume3DEditorBtn);
            this.threeDEditor.Controls.Add(this.start3DEditorBtn);
            this.threeDEditor.ForeColor = System.Drawing.SystemColors.ControlText;
            this.threeDEditor.Location = new System.Drawing.Point(4, 30);
            this.threeDEditor.Margin = new System.Windows.Forms.Padding(4);
            this.threeDEditor.Name = "threeDEditor";
            this.threeDEditor.Size = new System.Drawing.Size(1137, 387);
            this.threeDEditor.TabIndex = 3;
            this.threeDEditor.Text = "3D Editor";
            // 
            // resume3DEditorBtn
            // 
            this.resume3DEditorBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resume3DEditorBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.resume3DEditorBtn.Location = new System.Drawing.Point(457, 125);
            this.resume3DEditorBtn.Margin = new System.Windows.Forms.Padding(4);
            this.resume3DEditorBtn.Name = "resume3DEditorBtn";
            this.resume3DEditorBtn.Size = new System.Drawing.Size(267, 41);
            this.resume3DEditorBtn.TabIndex = 22;
            this.resume3DEditorBtn.Text = "Resume 3D Editor";
            this.resume3DEditorBtn.UseVisualStyleBackColor = true;
            // 
            // start3DEditorBtn
            // 
            this.start3DEditorBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.start3DEditorBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.start3DEditorBtn.Location = new System.Drawing.Point(457, 76);
            this.start3DEditorBtn.Margin = new System.Windows.Forms.Padding(4);
            this.start3DEditorBtn.Name = "start3DEditorBtn";
            this.start3DEditorBtn.Size = new System.Drawing.Size(267, 41);
            this.start3DEditorBtn.TabIndex = 21;
            this.start3DEditorBtn.Text = "Start 3D Editor";
            this.start3DEditorBtn.UseVisualStyleBackColor = true;
            // 
            // missionEditor
            // 
            this.missionEditor.BackColor = System.Drawing.SystemColors.Control;
            this.missionEditor.Controls.Add(this.missionLevelLbl);
            this.missionEditor.Controls.Add(this.missionAuthorLbl);
            this.missionEditor.Controls.Add(this.missionSizeLbl);
            this.missionEditor.Controls.Add(this.missionsOnlineDD);
            this.missionEditor.Controls.Add(this.uploadMissionBtn);
            this.missionEditor.Controls.Add(this.missionServerList);
            this.missionEditor.Controls.Add(this.missionLevelFlowTimerCb);
            this.missionEditor.Controls.Add(this.missionPlayTimeLbl);
            this.missionEditor.Controls.Add(this.addLevelFlowBtn);
            this.missionEditor.Controls.Add(this.downloadMissionBtn);
            this.missionEditor.Controls.Add(this.installMissionBtn);
            this.missionEditor.Controls.Add(this.missionDescTxt);
            this.missionEditor.Controls.Add(this.missionDescLbl);
            this.missionEditor.Controls.Add(this.missionRefreshBtn);
            this.missionEditor.Controls.Add(this.removeMissionBtn);
            this.missionEditor.Controls.Add(this.missionPlayTimeTxt);
            this.missionEditor.Controls.Add(this.missionNameTxt);
            this.missionEditor.Controls.Add(this.missionNameLbl);
            this.missionEditor.Controls.Add(this.loadMissionBtn);
            this.missionEditor.Controls.Add(this.saveMissionBtn);
            this.missionEditor.ForeColor = System.Drawing.SystemColors.ControlText;
            this.missionEditor.Location = new System.Drawing.Point(4, 30);
            this.missionEditor.Margin = new System.Windows.Forms.Padding(4);
            this.missionEditor.Name = "missionEditor";
            this.missionEditor.Size = new System.Drawing.Size(1137, 387);
            this.missionEditor.TabIndex = 4;
            this.missionEditor.Text = "Mission Editor";
            // 
            // missionLevelLbl
            // 
            this.missionLevelLbl.AutoSize = true;
            this.missionLevelLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.missionLevelLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.missionLevelLbl.Location = new System.Drawing.Point(447, 257);
            this.missionLevelLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.missionLevelLbl.Name = "missionLevelLbl";
            this.missionLevelLbl.Size = new System.Drawing.Size(59, 22);
            this.missionLevelLbl.TabIndex = 81;
            this.missionLevelLbl.Text = "Level";
            this.missionLevelLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // missionAuthorLbl
            // 
            this.missionAuthorLbl.AutoSize = true;
            this.missionAuthorLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.missionAuthorLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.missionAuthorLbl.Location = new System.Drawing.Point(225, 257);
            this.missionAuthorLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.missionAuthorLbl.Name = "missionAuthorLbl";
            this.missionAuthorLbl.Size = new System.Drawing.Size(84, 22);
            this.missionAuthorLbl.TabIndex = 80;
            this.missionAuthorLbl.Text = "Author: ";
            this.missionAuthorLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // missionSizeLbl
            // 
            this.missionSizeLbl.AutoSize = true;
            this.missionSizeLbl.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.missionSizeLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.missionSizeLbl.Location = new System.Drawing.Point(527, 257);
            this.missionSizeLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.missionSizeLbl.Name = "missionSizeLbl";
            this.missionSizeLbl.Size = new System.Drawing.Size(49, 21);
            this.missionSizeLbl.TabIndex = 79;
            this.missionSizeLbl.Text = "Size: ";
            this.missionSizeLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // missionsOnlineDD
            // 
            this.missionsOnlineDD.BackColor = System.Drawing.SystemColors.Control;
            this.missionsOnlineDD.Cursor = System.Windows.Forms.Cursors.Hand;
            this.missionsOnlineDD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.missionsOnlineDD.ForeColor = System.Drawing.SystemColors.WindowText;
            this.missionsOnlineDD.FormattingEnabled = true;
            this.missionsOnlineDD.Location = new System.Drawing.Point(229, 215);
            this.missionsOnlineDD.Name = "missionsOnlineDD";
            this.missionsOnlineDD.Size = new System.Drawing.Size(447, 29);
            this.missionsOnlineDD.TabIndex = 78;
            this.missionsOnlineDD.SelectedIndexChanged += new System.EventHandler(this.missionsOnlineDD_SelectedIndexChanged);
            // 
            // uploadMissionBtn
            // 
            this.uploadMissionBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.uploadMissionBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.uploadMissionBtn.Location = new System.Drawing.Point(451, 329);
            this.uploadMissionBtn.Margin = new System.Windows.Forms.Padding(4);
            this.uploadMissionBtn.Name = "uploadMissionBtn";
            this.uploadMissionBtn.Size = new System.Drawing.Size(225, 31);
            this.uploadMissionBtn.TabIndex = 77;
            this.uploadMissionBtn.Text = "Upload";
            this.uploadMissionBtn.UseVisualStyleBackColor = true;
            this.uploadMissionBtn.Click += new System.EventHandler(this.uploadMissionBtn_Click);
            // 
            // missionServerList
            // 
            this.missionServerList.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.missionServerList.ForeColor = System.Drawing.SystemColors.ControlText;
            this.missionServerList.Location = new System.Drawing.Point(44, 211);
            this.missionServerList.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.missionServerList.Name = "missionServerList";
            this.missionServerList.Size = new System.Drawing.Size(173, 33);
            this.missionServerList.TabIndex = 76;
            this.missionServerList.Text = "Missions Online";
            this.missionServerList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // missionLevelFlowTimerCb
            // 
            this.missionLevelFlowTimerCb.AutoSize = true;
            this.missionLevelFlowTimerCb.Checked = true;
            this.missionLevelFlowTimerCb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.missionLevelFlowTimerCb.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.missionLevelFlowTimerCb.Location = new System.Drawing.Point(809, 357);
            this.missionLevelFlowTimerCb.Margin = new System.Windows.Forms.Padding(4);
            this.missionLevelFlowTimerCb.Name = "missionLevelFlowTimerCb";
            this.missionLevelFlowTimerCb.Size = new System.Drawing.Size(118, 23);
            this.missionLevelFlowTimerCb.TabIndex = 74;
            this.missionLevelFlowTimerCb.Text = "Enable Timer";
            this.missionLevelFlowTimerCb.UseVisualStyleBackColor = true;
            // 
            // missionPlayTimeLbl
            // 
            this.missionPlayTimeLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.missionPlayTimeLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.missionPlayTimeLbl.Location = new System.Drawing.Point(805, 309);
            this.missionPlayTimeLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.missionPlayTimeLbl.Name = "missionPlayTimeLbl";
            this.missionPlayTimeLbl.Size = new System.Drawing.Size(122, 33);
            this.missionPlayTimeLbl.TabIndex = 36;
            this.missionPlayTimeLbl.Text = "Play Time";
            this.missionPlayTimeLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // addLevelFlowBtn
            // 
            this.addLevelFlowBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addLevelFlowBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.addLevelFlowBtn.Location = new System.Drawing.Point(946, 347);
            this.addLevelFlowBtn.Margin = new System.Windows.Forms.Padding(4);
            this.addLevelFlowBtn.Name = "addLevelFlowBtn";
            this.addLevelFlowBtn.Size = new System.Drawing.Size(183, 31);
            this.addLevelFlowBtn.TabIndex = 35;
            this.addLevelFlowBtn.Text = "Add Timer";
            this.addLevelFlowBtn.UseVisualStyleBackColor = true;
            this.addLevelFlowBtn.Click += new System.EventHandler(this.addLevelFlowBtn_Click);
            // 
            // downloadMissionBtn
            // 
            this.downloadMissionBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.downloadMissionBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.downloadMissionBtn.Location = new System.Drawing.Point(229, 329);
            this.downloadMissionBtn.Margin = new System.Windows.Forms.Padding(4);
            this.downloadMissionBtn.Name = "downloadMissionBtn";
            this.downloadMissionBtn.Size = new System.Drawing.Size(222, 31);
            this.downloadMissionBtn.TabIndex = 34;
            this.downloadMissionBtn.Text = "Download";
            this.downloadMissionBtn.UseVisualStyleBackColor = true;
            this.downloadMissionBtn.Click += new System.EventHandler(this.downloadMissionBtn_Click);
            // 
            // installMissionBtn
            // 
            this.installMissionBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.installMissionBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.installMissionBtn.Location = new System.Drawing.Point(712, 38);
            this.installMissionBtn.Margin = new System.Windows.Forms.Padding(4);
            this.installMissionBtn.Name = "installMissionBtn";
            this.installMissionBtn.Size = new System.Drawing.Size(210, 31);
            this.installMissionBtn.TabIndex = 34;
            this.installMissionBtn.Text = "Install Mission";
            this.installMissionBtn.UseVisualStyleBackColor = true;
            this.installMissionBtn.Click += new System.EventHandler(this.installMissionBtn_Click);
            // 
            // missionDescTxt
            // 
            this.missionDescTxt.BackColor = System.Drawing.SystemColors.Control;
            this.missionDescTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.missionDescTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.missionDescTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.missionDescTxt.Location = new System.Drawing.Point(226, 100);
            this.missionDescTxt.Margin = new System.Windows.Forms.Padding(4);
            this.missionDescTxt.Name = "missionDescTxt";
            this.missionDescTxt.Size = new System.Drawing.Size(450, 30);
            this.missionDescTxt.TabIndex = 33;
            // 
            // missionDescLbl
            // 
            this.missionDescLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.missionDescLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.missionDescLbl.Location = new System.Drawing.Point(52, 99);
            this.missionDescLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.missionDescLbl.Name = "missionDescLbl";
            this.missionDescLbl.Size = new System.Drawing.Size(173, 33);
            this.missionDescLbl.TabIndex = 32;
            this.missionDescLbl.Text = "Description ";
            this.missionDescLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // missionRefreshBtn
            // 
            this.missionRefreshBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.missionRefreshBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.missionRefreshBtn.Location = new System.Drawing.Point(712, 211);
            this.missionRefreshBtn.Margin = new System.Windows.Forms.Padding(4);
            this.missionRefreshBtn.Name = "missionRefreshBtn";
            this.missionRefreshBtn.Size = new System.Drawing.Size(210, 31);
            this.missionRefreshBtn.TabIndex = 28;
            this.missionRefreshBtn.Text = "Refresh Missions";
            this.missionRefreshBtn.UseVisualStyleBackColor = true;
            this.missionRefreshBtn.Click += new System.EventHandler(this.missionRefreshBtn_Click);
            // 
            // removeMissionBtn
            // 
            this.removeMissionBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.removeMissionBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.removeMissionBtn.Location = new System.Drawing.Point(712, 101);
            this.removeMissionBtn.Margin = new System.Windows.Forms.Padding(4);
            this.removeMissionBtn.Name = "removeMissionBtn";
            this.removeMissionBtn.Size = new System.Drawing.Size(210, 31);
            this.removeMissionBtn.TabIndex = 28;
            this.removeMissionBtn.Text = "Remove Mission";
            this.removeMissionBtn.UseVisualStyleBackColor = true;
            this.removeMissionBtn.Click += new System.EventHandler(this.removeMissionBtn_Click);
            // 
            // missionPlayTimeTxt
            // 
            this.missionPlayTimeTxt.BackColor = System.Drawing.SystemColors.Control;
            this.missionPlayTimeTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.missionPlayTimeTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.missionPlayTimeTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.missionPlayTimeTxt.Location = new System.Drawing.Point(946, 309);
            this.missionPlayTimeTxt.Margin = new System.Windows.Forms.Padding(4);
            this.missionPlayTimeTxt.Name = "missionPlayTimeTxt";
            this.missionPlayTimeTxt.Size = new System.Drawing.Size(183, 30);
            this.missionPlayTimeTxt.TabIndex = 30;
            this.missionPlayTimeTxt.Text = "60";
            // 
            // missionNameTxt
            // 
            this.missionNameTxt.BackColor = System.Drawing.SystemColors.Control;
            this.missionNameTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.missionNameTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.missionNameTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.missionNameTxt.Location = new System.Drawing.Point(226, 41);
            this.missionNameTxt.Margin = new System.Windows.Forms.Padding(4);
            this.missionNameTxt.Name = "missionNameTxt";
            this.missionNameTxt.Size = new System.Drawing.Size(450, 30);
            this.missionNameTxt.TabIndex = 30;
            // 
            // missionNameLbl
            // 
            this.missionNameLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.missionNameLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.missionNameLbl.Location = new System.Drawing.Point(52, 40);
            this.missionNameLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.missionNameLbl.Name = "missionNameLbl";
            this.missionNameLbl.Size = new System.Drawing.Size(173, 33);
            this.missionNameLbl.TabIndex = 29;
            this.missionNameLbl.Text = "Name ";
            this.missionNameLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // loadMissionBtn
            // 
            this.loadMissionBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loadMissionBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.loadMissionBtn.Location = new System.Drawing.Point(451, 150);
            this.loadMissionBtn.Margin = new System.Windows.Forms.Padding(4);
            this.loadMissionBtn.Name = "loadMissionBtn";
            this.loadMissionBtn.Size = new System.Drawing.Size(225, 31);
            this.loadMissionBtn.TabIndex = 28;
            this.loadMissionBtn.Text = "Load Mission";
            this.loadMissionBtn.UseVisualStyleBackColor = true;
            this.loadMissionBtn.Click += new System.EventHandler(this.loadMissionBtn_Click);
            // 
            // saveMissionBtn
            // 
            this.saveMissionBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveMissionBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.saveMissionBtn.Location = new System.Drawing.Point(226, 150);
            this.saveMissionBtn.Margin = new System.Windows.Forms.Padding(4);
            this.saveMissionBtn.Name = "saveMissionBtn";
            this.saveMissionBtn.Size = new System.Drawing.Size(225, 31);
            this.saveMissionBtn.TabIndex = 28;
            this.saveMissionBtn.Text = "Save Mission";
            this.saveMissionBtn.UseVisualStyleBackColor = true;
            this.saveMissionBtn.Click += new System.EventHandler(this.saveMissionBtn_Click);
            // 
            // graphEditor
            // 
            this.graphEditor.BackColor = System.Drawing.SystemColors.Control;
            this.graphEditor.Controls.Add(this.nodesInfoCb);
            this.graphEditor.Controls.Add(this.showAllGraphsCb);
            this.graphEditor.Controls.Add(this.gameItemsLbl);
            this.graphEditor.Controls.Add(this.stopTraversingNodesBtn);
            this.graphEditor.Controls.Add(this.manualTeleportGraphCb);
            this.graphEditor.Controls.Add(this.manualTeleportNodeCb);
            this.graphEditor.Controls.Add(this.autoTeleportGraphCb);
            this.graphEditor.Controls.Add(this.teleportToGraphBtn);
            this.graphEditor.Controls.Add(this.autoTeleportNodeCb);
            this.graphEditor.Controls.Add(this.viewPortCameraEnableCb);
            this.graphEditor.Controls.Add(this.teleportToNodeBtn);
            this.graphEditor.Controls.Add(this.nodeIdMetreCb);
            this.graphEditor.Controls.Add(this.nodeIdOffsetCb);
            this.graphEditor.Controls.Add(this.graphTotalNodesTxt);
            this.graphEditor.Controls.Add(this.graphTotalNodesLbl);
            this.graphEditor.Controls.Add(this.nodeCriteriaTxt);
            this.graphEditor.Controls.Add(this.nodeZTxt);
            this.graphEditor.Controls.Add(this.nodeZLbl);
            this.graphEditor.Controls.Add(this.nodeYTxt);
            this.graphEditor.Controls.Add(this.nodeYLbl);
            this.graphEditor.Controls.Add(this.nodeXTxt);
            this.graphEditor.Controls.Add(this.nodeXLbl);
            this.graphEditor.Controls.Add(this.removeNodesBtn);
            this.graphEditor.Controls.Add(this.removeLinksBtn);
            this.graphEditor.Controls.Add(this.refreshLinksBtn);
            this.graphEditor.Controls.Add(this.addLinksBtn);
            this.graphEditor.Controls.Add(this.refreshNodesBtn);
            this.graphEditor.Controls.Add(this.addNodesBtn);
            this.graphEditor.Controls.Add(this.graphsMarkCb);
            this.graphEditor.Controls.Add(this.graphsAllCb);
            this.graphEditor.Controls.Add(this.nodesHilightCb);
            this.graphEditor.Controls.Add(this.nodesObjectsCb);
            this.graphEditor.Controls.Add(this.nodeIdDD);
            this.graphEditor.Controls.Add(this.nodeIdLbl);
            this.graphEditor.Controls.Add(this.graphAreaLbl);
            this.graphEditor.Controls.Add(this.graphIdDD);
            this.graphEditor.Controls.Add(this.graphIdLbl);
            this.graphEditor.ForeColor = System.Drawing.SystemColors.ControlText;
            this.graphEditor.Location = new System.Drawing.Point(4, 30);
            this.graphEditor.Name = "graphEditor";
            this.graphEditor.Size = new System.Drawing.Size(1137, 387);
            this.graphEditor.TabIndex = 9;
            this.graphEditor.Text = "Graph Editor";
            // 
            // nodesInfoCb
            // 
            this.nodesInfoCb.AutoSize = true;
            this.nodesInfoCb.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nodesInfoCb.Location = new System.Drawing.Point(1002, 336);
            this.nodesInfoCb.Margin = new System.Windows.Forms.Padding(4);
            this.nodesInfoCb.Name = "nodesInfoCb";
            this.nodesInfoCb.Size = new System.Drawing.Size(112, 23);
            this.nodesInfoCb.TabIndex = 82;
            this.nodesInfoCb.Text = "Nodes - Info";
            this.nodesInfoCb.UseVisualStyleBackColor = true;
            this.nodesInfoCb.CheckedChanged += new System.EventHandler(this.nodesInfoCb_CheckedChanged);
            // 
            // showAllGraphsCb
            // 
            this.showAllGraphsCb.AutoSize = true;
            this.showAllGraphsCb.Font = new System.Drawing.Font("Century Gothic", 7.8F);
            this.showAllGraphsCb.Location = new System.Drawing.Point(342, 74);
            this.showAllGraphsCb.Margin = new System.Windows.Forms.Padding(4);
            this.showAllGraphsCb.Name = "showAllGraphsCb";
            this.showAllGraphsCb.Size = new System.Drawing.Size(100, 23);
            this.showAllGraphsCb.TabIndex = 81;
            this.showAllGraphsCb.Text = "All Graphs";
            this.showAllGraphsCb.UseVisualStyleBackColor = true;
            this.showAllGraphsCb.CheckedChanged += new System.EventHandler(this.showAllGraphsCb_CheckedChanged);
            // 
            // gameItemsLbl
            // 
            this.gameItemsLbl.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gameItemsLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gameItemsLbl.Location = new System.Drawing.Point(1002, 0);
            this.gameItemsLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.gameItemsLbl.Name = "gameItemsLbl";
            this.gameItemsLbl.Size = new System.Drawing.Size(127, 19);
            this.gameItemsLbl.TabIndex = 80;
            this.gameItemsLbl.Text = "Game Items";
            this.gameItemsLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.infoViewer.SetToolTip(this.gameItemsLbl, "Click to show Game items");
            this.gameItemsLbl.Click += new System.EventHandler(this.gameItemsLbl_Click);
            // 
            // stopTraversingNodesBtn
            // 
            this.stopTraversingNodesBtn.Enabled = false;
            this.stopTraversingNodesBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stopTraversingNodesBtn.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stopTraversingNodesBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.stopTraversingNodesBtn.Location = new System.Drawing.Point(636, 206);
            this.stopTraversingNodesBtn.Margin = new System.Windows.Forms.Padding(4);
            this.stopTraversingNodesBtn.Name = "stopTraversingNodesBtn";
            this.stopTraversingNodesBtn.Size = new System.Drawing.Size(147, 41);
            this.stopTraversingNodesBtn.TabIndex = 79;
            this.stopTraversingNodesBtn.Text = "Stop Traversing";
            this.stopTraversingNodesBtn.UseVisualStyleBackColor = true;
            this.stopTraversingNodesBtn.Click += new System.EventHandler(this.stopTraversingNodesBtn_Click);
            // 
            // manualTeleportGraphCb
            // 
            this.manualTeleportGraphCb.AutoSize = true;
            this.manualTeleportGraphCb.Checked = true;
            this.manualTeleportGraphCb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.manualTeleportGraphCb.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.manualTeleportGraphCb.Location = new System.Drawing.Point(51, 255);
            this.manualTeleportGraphCb.Margin = new System.Windows.Forms.Padding(4);
            this.manualTeleportGraphCb.Name = "manualTeleportGraphCb";
            this.manualTeleportGraphCb.Size = new System.Drawing.Size(139, 23);
            this.manualTeleportGraphCb.TabIndex = 78;
            this.manualTeleportGraphCb.Text = "Manual - Mode";
            this.manualTeleportGraphCb.UseVisualStyleBackColor = true;
            this.manualTeleportGraphCb.CheckedChanged += new System.EventHandler(this.manualTeleportGraphCb_CheckedChanged);
            // 
            // manualTeleportNodeCb
            // 
            this.manualTeleportNodeCb.AutoSize = true;
            this.manualTeleportNodeCb.Checked = true;
            this.manualTeleportNodeCb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.manualTeleportNodeCb.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.manualTeleportNodeCb.Location = new System.Drawing.Point(337, 255);
            this.manualTeleportNodeCb.Margin = new System.Windows.Forms.Padding(4);
            this.manualTeleportNodeCb.Name = "manualTeleportNodeCb";
            this.manualTeleportNodeCb.Size = new System.Drawing.Size(139, 23);
            this.manualTeleportNodeCb.TabIndex = 77;
            this.manualTeleportNodeCb.Text = "Manual - Mode";
            this.manualTeleportNodeCb.UseVisualStyleBackColor = true;
            this.manualTeleportNodeCb.CheckedChanged += new System.EventHandler(this.manualTeleportNodeCb_CheckedChanged);
            // 
            // autoTeleportGraphCb
            // 
            this.autoTeleportGraphCb.AutoSize = true;
            this.autoTeleportGraphCb.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoTeleportGraphCb.Location = new System.Drawing.Point(201, 255);
            this.autoTeleportGraphCb.Margin = new System.Windows.Forms.Padding(4);
            this.autoTeleportGraphCb.Name = "autoTeleportGraphCb";
            this.autoTeleportGraphCb.Size = new System.Drawing.Size(117, 23);
            this.autoTeleportGraphCb.TabIndex = 76;
            this.autoTeleportGraphCb.Text = "Auto - Mode";
            this.autoTeleportGraphCb.UseVisualStyleBackColor = true;
            this.autoTeleportGraphCb.CheckedChanged += new System.EventHandler(this.autoTeleportGraphCb_CheckedChanged);
            // 
            // teleportToGraphBtn
            // 
            this.teleportToGraphBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.teleportToGraphBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.teleportToGraphBtn.Location = new System.Drawing.Point(51, 206);
            this.teleportToGraphBtn.Margin = new System.Windows.Forms.Padding(4);
            this.teleportToGraphBtn.Name = "teleportToGraphBtn";
            this.teleportToGraphBtn.Size = new System.Drawing.Size(267, 41);
            this.teleportToGraphBtn.TabIndex = 75;
            this.teleportToGraphBtn.Text = "Teleport To Graph";
            this.teleportToGraphBtn.UseVisualStyleBackColor = true;
            this.teleportToGraphBtn.Click += new System.EventHandler(this.teleportToGraphBtn_Click);
            // 
            // autoTeleportNodeCb
            // 
            this.autoTeleportNodeCb.AutoSize = true;
            this.autoTeleportNodeCb.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoTeleportNodeCb.Location = new System.Drawing.Point(487, 255);
            this.autoTeleportNodeCb.Margin = new System.Windows.Forms.Padding(4);
            this.autoTeleportNodeCb.Name = "autoTeleportNodeCb";
            this.autoTeleportNodeCb.Size = new System.Drawing.Size(117, 23);
            this.autoTeleportNodeCb.TabIndex = 74;
            this.autoTeleportNodeCb.Text = "Auto - Mode";
            this.autoTeleportNodeCb.UseVisualStyleBackColor = true;
            this.autoTeleportNodeCb.CheckedChanged += new System.EventHandler(this.autoTeleportNodeCb_CheckedChanged);
            // 
            // viewPortCameraEnableCb
            // 
            this.viewPortCameraEnableCb.AutoSize = true;
            this.viewPortCameraEnableCb.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.viewPortCameraEnableCb.Location = new System.Drawing.Point(636, 255);
            this.viewPortCameraEnableCb.Margin = new System.Windows.Forms.Padding(4);
            this.viewPortCameraEnableCb.Name = "viewPortCameraEnableCb";
            this.viewPortCameraEnableCb.Size = new System.Drawing.Size(150, 23);
            this.viewPortCameraEnableCb.TabIndex = 73;
            this.viewPortCameraEnableCb.Text = "Camera - Enable";
            this.viewPortCameraEnableCb.UseVisualStyleBackColor = true;
            this.viewPortCameraEnableCb.CheckedChanged += new System.EventHandler(this.viewPortEnableCb_CheckedChanged);
            // 
            // teleportToNodeBtn
            // 
            this.teleportToNodeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.teleportToNodeBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.teleportToNodeBtn.Location = new System.Drawing.Point(342, 206);
            this.teleportToNodeBtn.Margin = new System.Windows.Forms.Padding(4);
            this.teleportToNodeBtn.Name = "teleportToNodeBtn";
            this.teleportToNodeBtn.Size = new System.Drawing.Size(267, 41);
            this.teleportToNodeBtn.TabIndex = 72;
            this.teleportToNodeBtn.Text = "Teleport To Node";
            this.teleportToNodeBtn.UseVisualStyleBackColor = true;
            this.teleportToNodeBtn.Click += new System.EventHandler(this.teleportToNodeBtn_Click);
            // 
            // nodeIdMetreCb
            // 
            this.nodeIdMetreCb.AutoSize = true;
            this.nodeIdMetreCb.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.nodeIdMetreCb.Location = new System.Drawing.Point(678, 154);
            this.nodeIdMetreCb.Margin = new System.Windows.Forms.Padding(4);
            this.nodeIdMetreCb.Name = "nodeIdMetreCb";
            this.nodeIdMetreCb.Size = new System.Drawing.Size(76, 24);
            this.nodeIdMetreCb.TabIndex = 70;
            this.nodeIdMetreCb.Text = "Meter";
            this.nodeIdMetreCb.UseVisualStyleBackColor = true;
            this.nodeIdMetreCb.CheckedChanged += new System.EventHandler(this.nodeIdMetreCb_CheckedChanged);
            // 
            // nodeIdOffsetCb
            // 
            this.nodeIdOffsetCb.AutoSize = true;
            this.nodeIdOffsetCb.Checked = true;
            this.nodeIdOffsetCb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.nodeIdOffsetCb.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.nodeIdOffsetCb.Location = new System.Drawing.Point(595, 154);
            this.nodeIdOffsetCb.Margin = new System.Windows.Forms.Padding(4);
            this.nodeIdOffsetCb.Name = "nodeIdOffsetCb";
            this.nodeIdOffsetCb.Size = new System.Drawing.Size(75, 24);
            this.nodeIdOffsetCb.TabIndex = 71;
            this.nodeIdOffsetCb.Text = "Offset";
            this.nodeIdOffsetCb.UseVisualStyleBackColor = true;
            this.nodeIdOffsetCb.CheckedChanged += new System.EventHandler(this.nodeIdOffsetCb_CheckedChanged);
            // 
            // graphTotalNodesTxt
            // 
            this.graphTotalNodesTxt.BackColor = System.Drawing.SystemColors.Control;
            this.graphTotalNodesTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.graphTotalNodesTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.graphTotalNodesTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.graphTotalNodesTxt.Location = new System.Drawing.Point(901, 40);
            this.graphTotalNodesTxt.Margin = new System.Windows.Forms.Padding(4);
            this.graphTotalNodesTxt.Name = "graphTotalNodesTxt";
            this.graphTotalNodesTxt.ReadOnly = true;
            this.graphTotalNodesTxt.Size = new System.Drawing.Size(142, 30);
            this.graphTotalNodesTxt.TabIndex = 69;
            // 
            // graphTotalNodesLbl
            // 
            this.graphTotalNodesLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.graphTotalNodesLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.graphTotalNodesLbl.Location = new System.Drawing.Point(699, 36);
            this.graphTotalNodesLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.graphTotalNodesLbl.Name = "graphTotalNodesLbl";
            this.graphTotalNodesLbl.Size = new System.Drawing.Size(165, 33);
            this.graphTotalNodesLbl.TabIndex = 68;
            this.graphTotalNodesLbl.Text = "Total Nodes";
            this.graphTotalNodesLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nodeCriteriaTxt
            // 
            this.nodeCriteriaTxt.BackColor = System.Drawing.SystemColors.Control;
            this.nodeCriteriaTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nodeCriteriaTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.nodeCriteriaTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.nodeCriteriaTxt.Location = new System.Drawing.Point(275, 115);
            this.nodeCriteriaTxt.Margin = new System.Windows.Forms.Padding(4);
            this.nodeCriteriaTxt.Name = "nodeCriteriaTxt";
            this.nodeCriteriaTxt.ReadOnly = true;
            this.nodeCriteriaTxt.Size = new System.Drawing.Size(234, 30);
            this.nodeCriteriaTxt.TabIndex = 67;
            // 
            // nodeZTxt
            // 
            this.nodeZTxt.BackColor = System.Drawing.SystemColors.Control;
            this.nodeZTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nodeZTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.nodeZTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.nodeZTxt.Location = new System.Drawing.Point(989, 116);
            this.nodeZTxt.Margin = new System.Windows.Forms.Padding(4);
            this.nodeZTxt.Name = "nodeZTxt";
            this.nodeZTxt.Size = new System.Drawing.Size(120, 30);
            this.nodeZTxt.TabIndex = 66;
            this.nodeZTxt.Text = "0";
            // 
            // nodeZLbl
            // 
            this.nodeZLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.nodeZLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.nodeZLbl.Location = new System.Drawing.Point(937, 113);
            this.nodeZLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.nodeZLbl.Name = "nodeZLbl";
            this.nodeZLbl.Size = new System.Drawing.Size(44, 33);
            this.nodeZLbl.TabIndex = 65;
            this.nodeZLbl.Text = "Z ";
            this.nodeZLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nodeYTxt
            // 
            this.nodeYTxt.BackColor = System.Drawing.SystemColors.Control;
            this.nodeYTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nodeYTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.nodeYTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.nodeYTxt.Location = new System.Drawing.Point(788, 116);
            this.nodeYTxt.Margin = new System.Windows.Forms.Padding(4);
            this.nodeYTxt.Name = "nodeYTxt";
            this.nodeYTxt.Size = new System.Drawing.Size(120, 30);
            this.nodeYTxt.TabIndex = 64;
            this.nodeYTxt.Text = "0";
            // 
            // nodeYLbl
            // 
            this.nodeYLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.nodeYLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.nodeYLbl.Location = new System.Drawing.Point(736, 113);
            this.nodeYLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.nodeYLbl.Name = "nodeYLbl";
            this.nodeYLbl.Size = new System.Drawing.Size(44, 33);
            this.nodeYLbl.TabIndex = 63;
            this.nodeYLbl.Text = "Y ";
            this.nodeYLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nodeXTxt
            // 
            this.nodeXTxt.BackColor = System.Drawing.SystemColors.Control;
            this.nodeXTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nodeXTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.nodeXTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.nodeXTxt.Location = new System.Drawing.Point(597, 116);
            this.nodeXTxt.Margin = new System.Windows.Forms.Padding(4);
            this.nodeXTxt.Name = "nodeXTxt";
            this.nodeXTxt.Size = new System.Drawing.Size(120, 30);
            this.nodeXTxt.TabIndex = 62;
            this.nodeXTxt.Text = "0";
            // 
            // nodeXLbl
            // 
            this.nodeXLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.nodeXLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.nodeXLbl.Location = new System.Drawing.Point(545, 113);
            this.nodeXLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.nodeXLbl.Name = "nodeXLbl";
            this.nodeXLbl.Size = new System.Drawing.Size(44, 33);
            this.nodeXLbl.TabIndex = 61;
            this.nodeXLbl.Text = "X ";
            this.nodeXLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // removeNodesBtn
            // 
            this.removeNodesBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.removeNodesBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.removeNodesBtn.Location = new System.Drawing.Point(51, 336);
            this.removeNodesBtn.Margin = new System.Windows.Forms.Padding(4);
            this.removeNodesBtn.Name = "removeNodesBtn";
            this.removeNodesBtn.Size = new System.Drawing.Size(267, 41);
            this.removeNodesBtn.TabIndex = 60;
            this.removeNodesBtn.Text = "Remove Node";
            this.removeNodesBtn.UseVisualStyleBackColor = true;
            this.removeNodesBtn.Click += new System.EventHandler(this.removeNodesBtn_Click);
            // 
            // removeLinksBtn
            // 
            this.removeLinksBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.removeLinksBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.removeLinksBtn.Location = new System.Drawing.Point(342, 336);
            this.removeLinksBtn.Margin = new System.Windows.Forms.Padding(4);
            this.removeLinksBtn.Name = "removeLinksBtn";
            this.removeLinksBtn.Size = new System.Drawing.Size(267, 41);
            this.removeLinksBtn.TabIndex = 60;
            this.removeLinksBtn.Text = "Remove Link";
            this.removeLinksBtn.UseVisualStyleBackColor = true;
            this.removeLinksBtn.Click += new System.EventHandler(this.removeLinksBtn_Click);
            // 
            // refreshLinksBtn
            // 
            this.refreshLinksBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refreshLinksBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.refreshLinksBtn.Location = new System.Drawing.Point(476, 295);
            this.refreshLinksBtn.Margin = new System.Windows.Forms.Padding(4);
            this.refreshLinksBtn.Name = "refreshLinksBtn";
            this.refreshLinksBtn.Size = new System.Drawing.Size(133, 41);
            this.refreshLinksBtn.TabIndex = 60;
            this.refreshLinksBtn.Text = "Refresh";
            this.refreshLinksBtn.UseVisualStyleBackColor = true;
            this.refreshLinksBtn.Click += new System.EventHandler(this.refreshLinksBtn_Click);
            // 
            // addLinksBtn
            // 
            this.addLinksBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addLinksBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.addLinksBtn.Location = new System.Drawing.Point(342, 295);
            this.addLinksBtn.Margin = new System.Windows.Forms.Padding(4);
            this.addLinksBtn.Name = "addLinksBtn";
            this.addLinksBtn.Size = new System.Drawing.Size(133, 41);
            this.addLinksBtn.TabIndex = 60;
            this.addLinksBtn.Text = "Add";
            this.addLinksBtn.UseVisualStyleBackColor = true;
            this.addLinksBtn.Click += new System.EventHandler(this.addLinksBtn_Click);
            // 
            // refreshNodesBtn
            // 
            this.refreshNodesBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refreshNodesBtn.Font = new System.Drawing.Font("Century Gothic", 9.45F);
            this.refreshNodesBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.refreshNodesBtn.Location = new System.Drawing.Point(185, 295);
            this.refreshNodesBtn.Margin = new System.Windows.Forms.Padding(4);
            this.refreshNodesBtn.Name = "refreshNodesBtn";
            this.refreshNodesBtn.Size = new System.Drawing.Size(133, 41);
            this.refreshNodesBtn.TabIndex = 60;
            this.refreshNodesBtn.Text = "Refresh";
            this.refreshNodesBtn.UseVisualStyleBackColor = true;
            this.refreshNodesBtn.Click += new System.EventHandler(this.refreshNodesBtn_Click);
            // 
            // addNodesBtn
            // 
            this.addNodesBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addNodesBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.addNodesBtn.Location = new System.Drawing.Point(51, 295);
            this.addNodesBtn.Margin = new System.Windows.Forms.Padding(4);
            this.addNodesBtn.Name = "addNodesBtn";
            this.addNodesBtn.Size = new System.Drawing.Size(133, 41);
            this.addNodesBtn.TabIndex = 60;
            this.addNodesBtn.Text = "Add";
            this.addNodesBtn.UseVisualStyleBackColor = true;
            this.addNodesBtn.Click += new System.EventHandler(this.addNodesBtn_Click);
            // 
            // graphsMarkCb
            // 
            this.graphsMarkCb.AutoSize = true;
            this.graphsMarkCb.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.graphsMarkCb.Location = new System.Drawing.Point(870, 336);
            this.graphsMarkCb.Margin = new System.Windows.Forms.Padding(4);
            this.graphsMarkCb.Name = "graphsMarkCb";
            this.graphsMarkCb.Size = new System.Drawing.Size(123, 23);
            this.graphsMarkCb.TabIndex = 58;
            this.graphsMarkCb.Text = "Graph - Mark";
            this.graphsMarkCb.UseVisualStyleBackColor = true;
            this.graphsMarkCb.CheckedChanged += new System.EventHandler(this.graphsMarkCb_CheckedChanged);
            // 
            // graphsAllCb
            // 
            this.graphsAllCb.AutoSize = true;
            this.graphsAllCb.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.graphsAllCb.Location = new System.Drawing.Point(870, 360);
            this.graphsAllCb.Margin = new System.Windows.Forms.Padding(4);
            this.graphsAllCb.Name = "graphsAllCb";
            this.graphsAllCb.Size = new System.Drawing.Size(104, 23);
            this.graphsAllCb.TabIndex = 58;
            this.graphsAllCb.Text = "Graph - All";
            this.graphsAllCb.UseVisualStyleBackColor = true;
            this.graphsAllCb.CheckedChanged += new System.EventHandler(this.nodesAllCb_CheckedChanged);
            // 
            // nodesHilightCb
            // 
            this.nodesHilightCb.AutoSize = true;
            this.nodesHilightCb.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nodesHilightCb.Location = new System.Drawing.Point(1001, 360);
            this.nodesHilightCb.Margin = new System.Windows.Forms.Padding(4);
            this.nodesHilightCb.Name = "nodesHilightCb";
            this.nodesHilightCb.Size = new System.Drawing.Size(129, 23);
            this.nodesHilightCb.TabIndex = 58;
            this.nodesHilightCb.Text = "Nodes - Hilight";
            this.nodesHilightCb.UseVisualStyleBackColor = true;
            this.nodesHilightCb.CheckedChanged += new System.EventHandler(this.nodesHilightCb_CheckedChanged);
            // 
            // nodesObjectsCb
            // 
            this.nodesObjectsCb.AutoSize = true;
            this.nodesObjectsCb.Checked = true;
            this.nodesObjectsCb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.nodesObjectsCb.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nodesObjectsCb.Location = new System.Drawing.Point(1002, 307);
            this.nodesObjectsCb.Margin = new System.Windows.Forms.Padding(4);
            this.nodesObjectsCb.Name = "nodesObjectsCb";
            this.nodesObjectsCb.Size = new System.Drawing.Size(131, 23);
            this.nodesObjectsCb.TabIndex = 57;
            this.nodesObjectsCb.Text = "Nodes- Object";
            this.nodesObjectsCb.UseVisualStyleBackColor = true;
            this.nodesObjectsCb.CheckedChanged += new System.EventHandler(this.nodesObjectsCb_CheckedChanged);
            // 
            // nodeIdDD
            // 
            this.nodeIdDD.BackColor = System.Drawing.SystemColors.Control;
            this.nodeIdDD.Cursor = System.Windows.Forms.Cursors.Hand;
            this.nodeIdDD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.nodeIdDD.ForeColor = System.Drawing.SystemColors.WindowText;
            this.nodeIdDD.FormattingEnabled = true;
            this.nodeIdDD.Location = new System.Drawing.Point(177, 112);
            this.nodeIdDD.Name = "nodeIdDD";
            this.nodeIdDD.Size = new System.Drawing.Size(73, 29);
            this.nodeIdDD.TabIndex = 56;
            this.nodeIdDD.SelectedIndexChanged += new System.EventHandler(this.nodeIdDD_SelectedIndexChanged);
            // 
            // nodeIdLbl
            // 
            this.nodeIdLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.nodeIdLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.nodeIdLbl.Location = new System.Drawing.Point(8, 108);
            this.nodeIdLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.nodeIdLbl.Name = "nodeIdLbl";
            this.nodeIdLbl.Size = new System.Drawing.Size(173, 33);
            this.nodeIdLbl.TabIndex = 55;
            this.nodeIdLbl.Text = "Node Id ";
            this.nodeIdLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // graphAreaLbl
            // 
            this.graphAreaLbl.BackColor = System.Drawing.SystemColors.Control;
            this.graphAreaLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.graphAreaLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.graphAreaLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.graphAreaLbl.Location = new System.Drawing.Point(342, 36);
            this.graphAreaLbl.Margin = new System.Windows.Forms.Padding(4);
            this.graphAreaLbl.Name = "graphAreaLbl";
            this.graphAreaLbl.ReadOnly = true;
            this.graphAreaLbl.Size = new System.Drawing.Size(328, 30);
            this.graphAreaLbl.TabIndex = 52;
            this.graphAreaLbl.Text = "Graph Area";
            // 
            // graphIdDD
            // 
            this.graphIdDD.BackColor = System.Drawing.SystemColors.Control;
            this.graphIdDD.Cursor = System.Windows.Forms.Cursors.Hand;
            this.graphIdDD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.graphIdDD.ForeColor = System.Drawing.SystemColors.WindowText;
            this.graphIdDD.FormattingEnabled = true;
            this.graphIdDD.Location = new System.Drawing.Point(177, 36);
            this.graphIdDD.Name = "graphIdDD";
            this.graphIdDD.Size = new System.Drawing.Size(73, 29);
            this.graphIdDD.TabIndex = 51;
            this.graphIdDD.SelectedIndexChanged += new System.EventHandler(this.graphIdDD_SelectedIndexChanged);
            // 
            // graphIdLbl
            // 
            this.graphIdLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.graphIdLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.graphIdLbl.Location = new System.Drawing.Point(4, 32);
            this.graphIdLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.graphIdLbl.Name = "graphIdLbl";
            this.graphIdLbl.Size = new System.Drawing.Size(173, 33);
            this.graphIdLbl.TabIndex = 50;
            this.graphIdLbl.Text = "Graph Id ";
            this.graphIdLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // positionEditor
            // 
            this.positionEditor.BackColor = System.Drawing.SystemColors.Control;
            this.positionEditor.Controls.Add(this.resetPosCb);
            this.positionEditor.Controls.Add(this.gammaTxt);
            this.positionEditor.Controls.Add(this.betaTxt);
            this.positionEditor.Controls.Add(this.alphaTxt);
            this.positionEditor.Controls.Add(this.gammaLbl);
            this.positionEditor.Controls.Add(this.betaLbl);
            this.positionEditor.Controls.Add(this.alphaLbl);
            this.positionEditor.Controls.Add(this.updateObjOrientation);
            this.positionEditor.Controls.Add(this.humanPosMeterCb);
            this.positionEditor.Controls.Add(this.humanPosOffCb);
            this.positionEditor.Controls.Add(this.zPosTxt_H);
            this.positionEditor.Controls.Add(this.zPosLbl_H);
            this.positionEditor.Controls.Add(this.yPosTxt_H);
            this.positionEditor.Controls.Add(this.yPosLbl_H);
            this.positionEditor.Controls.Add(this.xPosTxt_H);
            this.positionEditor.Controls.Add(this.xPosLbl_H);
            this.positionEditor.Controls.Add(this.updateHumaPosition);
            this.positionEditor.Controls.Add(this.objectPosDD);
            this.positionEditor.Controls.Add(this.objPosLbl);
            this.positionEditor.Controls.Add(this.buildingPosDD);
            this.positionEditor.Controls.Add(this.buildingPosLbl);
            this.positionEditor.Controls.Add(this.posCurrentCb);
            this.positionEditor.Controls.Add(this.posMeterCb);
            this.positionEditor.Controls.Add(this.posOffCb);
            this.positionEditor.Controls.Add(this.zPosObjTxt);
            this.positionEditor.Controls.Add(this.zPosLbl_O);
            this.positionEditor.Controls.Add(this.yPosObjTxt);
            this.positionEditor.Controls.Add(this.yPosLbl_O);
            this.positionEditor.Controls.Add(this.xPosObjTxt);
            this.positionEditor.Controls.Add(this.xPosLbl_O);
            this.positionEditor.Controls.Add(this.updateObjPosition);
            this.positionEditor.ForeColor = System.Drawing.SystemColors.ControlText;
            this.positionEditor.Location = new System.Drawing.Point(4, 30);
            this.positionEditor.Margin = new System.Windows.Forms.Padding(4);
            this.positionEditor.Name = "positionEditor";
            this.positionEditor.Size = new System.Drawing.Size(1137, 387);
            this.positionEditor.TabIndex = 5;
            this.positionEditor.Text = "Position Editor";
            // 
            // resetPosCb
            // 
            this.resetPosCb.AutoSize = true;
            this.resetPosCb.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.resetPosCb.ForeColor = System.Drawing.SystemColors.ControlText;
            this.resetPosCb.Location = new System.Drawing.Point(1019, 332);
            this.resetPosCb.Margin = new System.Windows.Forms.Padding(4);
            this.resetPosCb.Name = "resetPosCb";
            this.resetPosCb.Size = new System.Drawing.Size(100, 24);
            this.resetPosCb.TabIndex = 73;
            this.resetPosCb.Text = "Reset Pos";
            this.resetPosCb.UseVisualStyleBackColor = true;
            this.resetPosCb.CheckedChanged += new System.EventHandler(this.resetPosCb_CheckedChanged);
            // 
            // gammaTxt
            // 
            this.gammaTxt.BackColor = System.Drawing.SystemColors.Control;
            this.gammaTxt.DecimalPlaces = 4;
            this.gammaTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.gammaTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gammaTxt.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.gammaTxt.Location = new System.Drawing.Point(691, 208);
            this.gammaTxt.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.gammaTxt.Name = "gammaTxt";
            this.gammaTxt.Size = new System.Drawing.Size(120, 30);
            this.gammaTxt.TabIndex = 70;
            // 
            // betaTxt
            // 
            this.betaTxt.BackColor = System.Drawing.SystemColors.Control;
            this.betaTxt.DecimalPlaces = 4;
            this.betaTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.betaTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.betaTxt.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.betaTxt.Location = new System.Drawing.Point(426, 208);
            this.betaTxt.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.betaTxt.Name = "betaTxt";
            this.betaTxt.Size = new System.Drawing.Size(120, 30);
            this.betaTxt.TabIndex = 71;
            // 
            // alphaTxt
            // 
            this.alphaTxt.BackColor = System.Drawing.SystemColors.Control;
            this.alphaTxt.DecimalPlaces = 4;
            this.alphaTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.alphaTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.alphaTxt.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.alphaTxt.Location = new System.Drawing.Point(174, 208);
            this.alphaTxt.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.alphaTxt.Name = "alphaTxt";
            this.alphaTxt.Size = new System.Drawing.Size(120, 30);
            this.alphaTxt.TabIndex = 72;
            // 
            // gammaLbl
            // 
            this.gammaLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.gammaLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gammaLbl.Location = new System.Drawing.Point(569, 206);
            this.gammaLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.gammaLbl.Name = "gammaLbl";
            this.gammaLbl.Size = new System.Drawing.Size(104, 33);
            this.gammaLbl.TabIndex = 69;
            this.gammaLbl.Text = "Gamma ";
            this.gammaLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // betaLbl
            // 
            this.betaLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.betaLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.betaLbl.Location = new System.Drawing.Point(325, 204);
            this.betaLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.betaLbl.Name = "betaLbl";
            this.betaLbl.Size = new System.Drawing.Size(93, 33);
            this.betaLbl.TabIndex = 68;
            this.betaLbl.Text = "Beta ";
            this.betaLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // alphaLbl
            // 
            this.alphaLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.alphaLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.alphaLbl.Location = new System.Drawing.Point(74, 202);
            this.alphaLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.alphaLbl.Name = "alphaLbl";
            this.alphaLbl.Size = new System.Drawing.Size(93, 33);
            this.alphaLbl.TabIndex = 67;
            this.alphaLbl.Text = "Alpha ";
            this.alphaLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // updateObjOrientation
            // 
            this.updateObjOrientation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.updateObjOrientation.ForeColor = System.Drawing.SystemColors.ControlText;
            this.updateObjOrientation.Location = new System.Drawing.Point(851, 207);
            this.updateObjOrientation.Margin = new System.Windows.Forms.Padding(4);
            this.updateObjOrientation.Name = "updateObjOrientation";
            this.updateObjOrientation.Size = new System.Drawing.Size(277, 31);
            this.updateObjOrientation.TabIndex = 66;
            this.updateObjOrientation.Text = "Update Object Orientation";
            this.updateObjOrientation.UseVisualStyleBackColor = true;
            this.updateObjOrientation.Click += new System.EventHandler(this.updateObjOrientation_Click);
            // 
            // humanPosMeterCb
            // 
            this.humanPosMeterCb.AutoSize = true;
            this.humanPosMeterCb.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.humanPosMeterCb.Location = new System.Drawing.Point(929, 332);
            this.humanPosMeterCb.Margin = new System.Windows.Forms.Padding(4);
            this.humanPosMeterCb.Name = "humanPosMeterCb";
            this.humanPosMeterCb.Size = new System.Drawing.Size(76, 24);
            this.humanPosMeterCb.TabIndex = 64;
            this.humanPosMeterCb.Text = "Meter";
            this.humanPosMeterCb.UseVisualStyleBackColor = true;
            this.humanPosMeterCb.CheckedChanged += new System.EventHandler(this.humanPosMeterCb_CheckedChanged);
            // 
            // humanPosOffCb
            // 
            this.humanPosOffCb.AutoSize = true;
            this.humanPosOffCb.Checked = true;
            this.humanPosOffCb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.humanPosOffCb.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.humanPosOffCb.Location = new System.Drawing.Point(851, 332);
            this.humanPosOffCb.Margin = new System.Windows.Forms.Padding(4);
            this.humanPosOffCb.Name = "humanPosOffCb";
            this.humanPosOffCb.Size = new System.Drawing.Size(75, 24);
            this.humanPosOffCb.TabIndex = 65;
            this.humanPosOffCb.Text = "Offset";
            this.humanPosOffCb.UseVisualStyleBackColor = true;
            this.humanPosOffCb.CheckedChanged += new System.EventHandler(this.humanPosOffCb_CheckedChanged);
            // 
            // zPosTxt_H
            // 
            this.zPosTxt_H.BackColor = System.Drawing.SystemColors.Control;
            this.zPosTxt_H.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.zPosTxt_H.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.zPosTxt_H.ForeColor = System.Drawing.SystemColors.ControlText;
            this.zPosTxt_H.Location = new System.Drawing.Point(691, 295);
            this.zPosTxt_H.Margin = new System.Windows.Forms.Padding(4);
            this.zPosTxt_H.Name = "zPosTxt_H";
            this.zPosTxt_H.Size = new System.Drawing.Size(120, 30);
            this.zPosTxt_H.TabIndex = 63;
            this.zPosTxt_H.Text = "0";
            // 
            // zPosLbl_H
            // 
            this.zPosLbl_H.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.zPosLbl_H.ForeColor = System.Drawing.SystemColors.ControlText;
            this.zPosLbl_H.Location = new System.Drawing.Point(579, 293);
            this.zPosLbl_H.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.zPosLbl_H.Name = "zPosLbl_H";
            this.zPosLbl_H.Size = new System.Drawing.Size(104, 33);
            this.zPosLbl_H.TabIndex = 62;
            this.zPosLbl_H.Text = "Z ";
            this.zPosLbl_H.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // yPosTxt_H
            // 
            this.yPosTxt_H.BackColor = System.Drawing.SystemColors.Control;
            this.yPosTxt_H.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.yPosTxt_H.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.yPosTxt_H.ForeColor = System.Drawing.SystemColors.ControlText;
            this.yPosTxt_H.Location = new System.Drawing.Point(426, 292);
            this.yPosTxt_H.Margin = new System.Windows.Forms.Padding(4);
            this.yPosTxt_H.Name = "yPosTxt_H";
            this.yPosTxt_H.Size = new System.Drawing.Size(120, 30);
            this.yPosTxt_H.TabIndex = 61;
            this.yPosTxt_H.Text = "0";
            // 
            // yPosLbl_H
            // 
            this.yPosLbl_H.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.yPosLbl_H.ForeColor = System.Drawing.SystemColors.ControlText;
            this.yPosLbl_H.Location = new System.Drawing.Point(335, 291);
            this.yPosLbl_H.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.yPosLbl_H.Name = "yPosLbl_H";
            this.yPosLbl_H.Size = new System.Drawing.Size(93, 33);
            this.yPosLbl_H.TabIndex = 60;
            this.yPosLbl_H.Text = "Y ";
            this.yPosLbl_H.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // xPosTxt_H
            // 
            this.xPosTxt_H.BackColor = System.Drawing.SystemColors.Control;
            this.xPosTxt_H.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.xPosTxt_H.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.xPosTxt_H.ForeColor = System.Drawing.SystemColors.ControlText;
            this.xPosTxt_H.Location = new System.Drawing.Point(174, 290);
            this.xPosTxt_H.Margin = new System.Windows.Forms.Padding(4);
            this.xPosTxt_H.Name = "xPosTxt_H";
            this.xPosTxt_H.Size = new System.Drawing.Size(120, 30);
            this.xPosTxt_H.TabIndex = 59;
            this.xPosTxt_H.Text = "0";
            // 
            // xPosLbl_H
            // 
            this.xPosLbl_H.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.xPosLbl_H.ForeColor = System.Drawing.SystemColors.ControlText;
            this.xPosLbl_H.Location = new System.Drawing.Point(84, 289);
            this.xPosLbl_H.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.xPosLbl_H.Name = "xPosLbl_H";
            this.xPosLbl_H.Size = new System.Drawing.Size(93, 33);
            this.xPosLbl_H.TabIndex = 58;
            this.xPosLbl_H.Text = "X ";
            this.xPosLbl_H.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // updateHumaPosition
            // 
            this.updateHumaPosition.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.updateHumaPosition.ForeColor = System.Drawing.SystemColors.ControlText;
            this.updateHumaPosition.Location = new System.Drawing.Point(851, 293);
            this.updateHumaPosition.Margin = new System.Windows.Forms.Padding(4);
            this.updateHumaPosition.Name = "updateHumaPosition";
            this.updateHumaPosition.Size = new System.Drawing.Size(277, 31);
            this.updateHumaPosition.TabIndex = 57;
            this.updateHumaPosition.Text = "Update Human Position";
            this.updateHumaPosition.UseVisualStyleBackColor = true;
            this.updateHumaPosition.Click += new System.EventHandler(this.updateHumaPosition_Click);
            // 
            // objectPosDD
            // 
            this.objectPosDD.BackColor = System.Drawing.SystemColors.Control;
            this.objectPosDD.Cursor = System.Windows.Forms.Cursors.Hand;
            this.objectPosDD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.objectPosDD.ForeColor = System.Drawing.SystemColors.WindowText;
            this.objectPosDD.FormattingEnabled = true;
            this.objectPosDD.Location = new System.Drawing.Point(671, 37);
            this.objectPosDD.Name = "objectPosDD";
            this.objectPosDD.Size = new System.Drawing.Size(419, 29);
            this.objectPosDD.TabIndex = 56;
            this.objectPosDD.SelectedIndexChanged += new System.EventHandler(this.objectPosDD_SelectedIndexChanged);
            this.objectPosDD.Click += new System.EventHandler(this.objectSelectDD_Click);
            // 
            // objPosLbl
            // 
            this.objPosLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.objPosLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.objPosLbl.Location = new System.Drawing.Point(510, 37);
            this.objPosLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.objPosLbl.Name = "objPosLbl";
            this.objPosLbl.Size = new System.Drawing.Size(173, 33);
            this.objPosLbl.TabIndex = 55;
            this.objPosLbl.Text = "Object";
            this.objPosLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buildingPosDD
            // 
            this.buildingPosDD.BackColor = System.Drawing.SystemColors.Control;
            this.buildingPosDD.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buildingPosDD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.buildingPosDD.ForeColor = System.Drawing.SystemColors.WindowText;
            this.buildingPosDD.FormattingEnabled = true;
            this.buildingPosDD.Location = new System.Drawing.Point(151, 37);
            this.buildingPosDD.Name = "buildingPosDD";
            this.buildingPosDD.Size = new System.Drawing.Size(360, 29);
            this.buildingPosDD.TabIndex = 54;
            this.buildingPosDD.SelectedIndexChanged += new System.EventHandler(this.buildingPosDD_SelectedIndexChanged);
            this.buildingPosDD.Click += new System.EventHandler(this.buildingSelectDD_Click);
            // 
            // buildingPosLbl
            // 
            this.buildingPosLbl.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.buildingPosLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buildingPosLbl.Location = new System.Drawing.Point(11, 34);
            this.buildingPosLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.buildingPosLbl.Name = "buildingPosLbl";
            this.buildingPosLbl.Size = new System.Drawing.Size(173, 33);
            this.buildingPosLbl.TabIndex = 53;
            this.buildingPosLbl.Text = "Building";
            this.buildingPosLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // posCurrentCb
            // 
            this.posCurrentCb.AutoSize = true;
            this.posCurrentCb.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.posCurrentCb.ForeColor = System.Drawing.SystemColors.ControlText;
            this.posCurrentCb.Location = new System.Drawing.Point(1013, 152);
            this.posCurrentCb.Margin = new System.Windows.Forms.Padding(4);
            this.posCurrentCb.Name = "posCurrentCb";
            this.posCurrentCb.Size = new System.Drawing.Size(115, 24);
            this.posCurrentCb.TabIndex = 52;
            this.posCurrentCb.Text = "Current Pos";
            this.posCurrentCb.UseVisualStyleBackColor = true;
            this.posCurrentCb.CheckedChanged += new System.EventHandler(this.posCurrentCb_CheckedChanged);
            // 
            // posMeterCb
            // 
            this.posMeterCb.AutoSize = true;
            this.posMeterCb.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.posMeterCb.Location = new System.Drawing.Point(929, 152);
            this.posMeterCb.Margin = new System.Windows.Forms.Padding(4);
            this.posMeterCb.Name = "posMeterCb";
            this.posMeterCb.Size = new System.Drawing.Size(76, 24);
            this.posMeterCb.TabIndex = 50;
            this.posMeterCb.Text = "Meter";
            this.posMeterCb.UseVisualStyleBackColor = true;
            this.posMeterCb.CheckStateChanged += new System.EventHandler(this.posMeterCb_CheckedChanged);
            // 
            // posOffCb
            // 
            this.posOffCb.AutoSize = true;
            this.posOffCb.Checked = true;
            this.posOffCb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.posOffCb.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.posOffCb.Location = new System.Drawing.Point(851, 152);
            this.posOffCb.Margin = new System.Windows.Forms.Padding(4);
            this.posOffCb.Name = "posOffCb";
            this.posOffCb.Size = new System.Drawing.Size(75, 24);
            this.posOffCb.TabIndex = 51;
            this.posOffCb.Text = "Offset";
            this.posOffCb.UseVisualStyleBackColor = true;
            this.posOffCb.CheckedChanged += new System.EventHandler(this.posOffCb_CheckedChanged);
            // 
            // zPosObjTxt
            // 
            this.zPosObjTxt.BackColor = System.Drawing.SystemColors.Control;
            this.zPosObjTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.zPosObjTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.zPosObjTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.zPosObjTxt.Location = new System.Drawing.Point(691, 113);
            this.zPosObjTxt.Margin = new System.Windows.Forms.Padding(4);
            this.zPosObjTxt.Name = "zPosObjTxt";
            this.zPosObjTxt.Size = new System.Drawing.Size(120, 30);
            this.zPosObjTxt.TabIndex = 49;
            this.zPosObjTxt.Text = "0";
            // 
            // zPosLbl_O
            // 
            this.zPosLbl_O.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.zPosLbl_O.ForeColor = System.Drawing.SystemColors.ControlText;
            this.zPosLbl_O.Location = new System.Drawing.Point(579, 111);
            this.zPosLbl_O.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.zPosLbl_O.Name = "zPosLbl_O";
            this.zPosLbl_O.Size = new System.Drawing.Size(104, 33);
            this.zPosLbl_O.TabIndex = 48;
            this.zPosLbl_O.Text = "Z ";
            this.zPosLbl_O.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // yPosObjTxt
            // 
            this.yPosObjTxt.BackColor = System.Drawing.SystemColors.Control;
            this.yPosObjTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.yPosObjTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.yPosObjTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.yPosObjTxt.Location = new System.Drawing.Point(426, 110);
            this.yPosObjTxt.Margin = new System.Windows.Forms.Padding(4);
            this.yPosObjTxt.Name = "yPosObjTxt";
            this.yPosObjTxt.Size = new System.Drawing.Size(120, 30);
            this.yPosObjTxt.TabIndex = 47;
            this.yPosObjTxt.Text = "0";
            // 
            // yPosLbl_O
            // 
            this.yPosLbl_O.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.yPosLbl_O.ForeColor = System.Drawing.SystemColors.ControlText;
            this.yPosLbl_O.Location = new System.Drawing.Point(335, 109);
            this.yPosLbl_O.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.yPosLbl_O.Name = "yPosLbl_O";
            this.yPosLbl_O.Size = new System.Drawing.Size(93, 33);
            this.yPosLbl_O.TabIndex = 46;
            this.yPosLbl_O.Text = "Y ";
            this.yPosLbl_O.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // xPosObjTxt
            // 
            this.xPosObjTxt.BackColor = System.Drawing.SystemColors.Control;
            this.xPosObjTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.xPosObjTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.xPosObjTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.xPosObjTxt.Location = new System.Drawing.Point(174, 108);
            this.xPosObjTxt.Margin = new System.Windows.Forms.Padding(4);
            this.xPosObjTxt.Name = "xPosObjTxt";
            this.xPosObjTxt.Size = new System.Drawing.Size(120, 30);
            this.xPosObjTxt.TabIndex = 45;
            this.xPosObjTxt.Text = "0";
            // 
            // xPosLbl_O
            // 
            this.xPosLbl_O.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.xPosLbl_O.ForeColor = System.Drawing.SystemColors.ControlText;
            this.xPosLbl_O.Location = new System.Drawing.Point(84, 107);
            this.xPosLbl_O.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.xPosLbl_O.Name = "xPosLbl_O";
            this.xPosLbl_O.Size = new System.Drawing.Size(93, 33);
            this.xPosLbl_O.TabIndex = 44;
            this.xPosLbl_O.Text = "X ";
            this.xPosLbl_O.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // updateObjPosition
            // 
            this.updateObjPosition.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.updateObjPosition.ForeColor = System.Drawing.SystemColors.ControlText;
            this.updateObjPosition.Location = new System.Drawing.Point(851, 113);
            this.updateObjPosition.Margin = new System.Windows.Forms.Padding(4);
            this.updateObjPosition.Name = "updateObjPosition";
            this.updateObjPosition.Size = new System.Drawing.Size(277, 31);
            this.updateObjPosition.TabIndex = 43;
            this.updateObjPosition.Text = "Update Object Position";
            this.updateObjPosition.UseVisualStyleBackColor = true;
            this.updateObjPosition.Click += new System.EventHandler(this.updateObjPosition_Click);
            // 
            // miscEditor
            // 
            this.miscEditor.BackColor = System.Drawing.SystemColors.Control;
            this.miscEditor.Controls.Add(this.viewLogsCb);
            this.miscEditor.Controls.Add(this.shareLogsCb);
            this.miscEditor.Controls.Add(this.externalCompilerCb);
            this.miscEditor.Controls.Add(this.internalCompilerCb);
            this.miscEditor.Controls.Add(this.framesTxt);
            this.miscEditor.Controls.Add(this.minutesLbl);
            this.miscEditor.Controls.Add(this.showAppDataCachePathBtn);
            this.miscEditor.Controls.Add(this.gamePathBtn);
            this.miscEditor.Controls.Add(this.editorUpdaterBtn);
            this.miscEditor.Controls.Add(this.gameSettingsLbl);
            this.miscEditor.Controls.Add(this.appSettingLbl);
            this.miscEditor.Controls.Add(this.configLoadBtn);
            this.miscEditor.Controls.Add(this.configSaveBtn);
            this.miscEditor.Controls.Add(this.updateIntervalTxtx);
            this.miscEditor.Controls.Add(this.musicTrackBar);
            this.miscEditor.Controls.Add(this.musicSoundCb);
            this.miscEditor.Controls.Add(this.sfxMusicCb);
            this.miscEditor.Controls.Add(this.enableMusicCb);
            this.miscEditor.Controls.Add(this.debugModeCb);
            this.miscEditor.Controls.Add(this.udpateMusicBtn);
            this.miscEditor.Controls.Add(this.gfxResetBtn);
            this.miscEditor.Controls.Add(this.setFramesBtn);
            this.miscEditor.Controls.Add(this.aiIdleCb);
            this.miscEditor.Controls.Add(this.appSupportBtn);
            this.miscEditor.Controls.Add(this.disableWarningsCb);
            this.miscEditor.Controls.Add(this.showAppLogBtn);
            this.miscEditor.Controls.Add(this.xmlCb);
            this.miscEditor.Controls.Add(this.jsonCb);
            this.miscEditor.Controls.Add(this.csvCb);
            this.miscEditor.Controls.Add(this.updateCheckerCb);
            this.miscEditor.Controls.Add(this.appLogsCb);
            this.miscEditor.Controls.Add(this.autoRefreshGameCb);
            this.miscEditor.Controls.Add(this.autoResetCb);
            this.miscEditor.Controls.Add(this.cutsceneRemoveBtn);
            this.miscEditor.Controls.Add(this.resetScriptsFileBtn);
            this.miscEditor.Controls.Add(this.resetLevelBtn);
            this.miscEditor.Controls.Add(this.clearCacheBtn);
            this.miscEditor.Controls.Add(this.exportObjectsBtn);
            this.miscEditor.ForeColor = System.Drawing.SystemColors.ControlText;
            this.miscEditor.Location = new System.Drawing.Point(4, 30);
            this.miscEditor.Name = "miscEditor";
            this.miscEditor.Size = new System.Drawing.Size(1137, 387);
            this.miscEditor.TabIndex = 6;
            this.miscEditor.Text = "Misc";
            // 
            // viewLogsCb
            // 
            this.viewLogsCb.AutoSize = true;
            this.viewLogsCb.Checked = true;
            this.viewLogsCb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.viewLogsCb.Font = new System.Drawing.Font("Century Gothic", 8F);
            this.viewLogsCb.Location = new System.Drawing.Point(162, 233);
            this.viewLogsCb.Margin = new System.Windows.Forms.Padding(4);
            this.viewLogsCb.Name = "viewLogsCb";
            this.viewLogsCb.Size = new System.Drawing.Size(64, 23);
            this.viewLogsCb.TabIndex = 58;
            this.viewLogsCb.Text = "View";
            this.viewLogsCb.UseVisualStyleBackColor = true;
            this.viewLogsCb.CheckedChanged += new System.EventHandler(this.viewLogsCb_CheckedChanged);
            // 
            // shareLogsCb
            // 
            this.shareLogsCb.AutoSize = true;
            this.shareLogsCb.Font = new System.Drawing.Font("Century Gothic", 8F);
            this.shareLogsCb.Location = new System.Drawing.Point(162, 214);
            this.shareLogsCb.Margin = new System.Windows.Forms.Padding(4);
            this.shareLogsCb.Name = "shareLogsCb";
            this.shareLogsCb.Size = new System.Drawing.Size(70, 23);
            this.shareLogsCb.TabIndex = 57;
            this.shareLogsCb.Text = "Share";
            this.shareLogsCb.UseVisualStyleBackColor = true;
            this.shareLogsCb.CheckedChanged += new System.EventHandler(this.shareLogsCb_CheckedChanged);
            // 
            // externalCompilerCb
            // 
            this.externalCompilerCb.AutoSize = true;
            this.externalCompilerCb.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.externalCompilerCb.Location = new System.Drawing.Point(149, 354);
            this.externalCompilerCb.Margin = new System.Windows.Forms.Padding(4);
            this.externalCompilerCb.Name = "externalCompilerCb";
            this.externalCompilerCb.Size = new System.Drawing.Size(86, 23);
            this.externalCompilerCb.TabIndex = 56;
            this.externalCompilerCb.Text = "External";
            this.externalCompilerCb.UseVisualStyleBackColor = true;
            this.externalCompilerCb.CheckedChanged += new System.EventHandler(this.externalCompilerCb_CheckedChanged);
            // 
            // internalCompilerCb
            // 
            this.internalCompilerCb.AutoSize = true;
            this.internalCompilerCb.Checked = true;
            this.internalCompilerCb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.internalCompilerCb.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.internalCompilerCb.Location = new System.Drawing.Point(42, 354);
            this.internalCompilerCb.Margin = new System.Windows.Forms.Padding(4);
            this.internalCompilerCb.Name = "internalCompilerCb";
            this.internalCompilerCb.Size = new System.Drawing.Size(83, 23);
            this.internalCompilerCb.TabIndex = 56;
            this.internalCompilerCb.Text = "Internal";
            this.internalCompilerCb.UseVisualStyleBackColor = true;
            this.internalCompilerCb.CheckedChanged += new System.EventHandler(this.internalCompilerCb_CheckedChanged);
            // 
            // framesTxt
            // 
            this.framesTxt.BackColor = System.Drawing.SystemColors.Control;
            this.framesTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.framesTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.framesTxt.Location = new System.Drawing.Point(980, 56);
            this.framesTxt.Maximum = new decimal(new int[] {
            240,
            0,
            0,
            0});
            this.framesTxt.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.framesTxt.Name = "framesTxt";
            this.framesTxt.Size = new System.Drawing.Size(101, 30);
            this.framesTxt.TabIndex = 45;
            this.framesTxt.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.framesTxt.ValueChanged += new System.EventHandler(this.framesTxt_ValueChanged);
            // 
            // minutesLbl
            // 
            this.minutesLbl.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minutesLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.minutesLbl.Location = new System.Drawing.Point(334, 177);
            this.minutesLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.minutesLbl.Name = "minutesLbl";
            this.minutesLbl.Size = new System.Drawing.Size(96, 33);
            this.minutesLbl.TabIndex = 44;
            this.minutesLbl.Text = "minutes";
            this.minutesLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.minutesLbl.MouseEnter += new System.EventHandler(this.minutesLbl_MouseEnter);
            // 
            // showAppDataCachePathBtn
            // 
            this.showAppDataCachePathBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.showAppDataCachePathBtn.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.showAppDataCachePathBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.showAppDataCachePathBtn.Location = new System.Drawing.Point(42, 264);
            this.showAppDataCachePathBtn.Margin = new System.Windows.Forms.Padding(4);
            this.showAppDataCachePathBtn.Name = "showAppDataCachePathBtn";
            this.showAppDataCachePathBtn.Size = new System.Drawing.Size(190, 38);
            this.showAppDataCachePathBtn.TabIndex = 41;
            this.showAppDataCachePathBtn.Text = "Show AppData Path";
            this.showAppDataCachePathBtn.UseVisualStyleBackColor = true;
            this.showAppDataCachePathBtn.Click += new System.EventHandler(this.showAppDataCachePathBtn_Click);
            this.showAppDataCachePathBtn.MouseEnter += new System.EventHandler(this.editorUpdaterBtn_MouseEnter);
            // 
            // gamePathBtn
            // 
            this.gamePathBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gamePathBtn.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.gamePathBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gamePathBtn.Location = new System.Drawing.Point(267, 262);
            this.gamePathBtn.Margin = new System.Windows.Forms.Padding(4);
            this.gamePathBtn.Name = "gamePathBtn";
            this.gamePathBtn.Size = new System.Drawing.Size(190, 38);
            this.gamePathBtn.TabIndex = 41;
            this.gamePathBtn.Text = "Set Game Path";
            this.gamePathBtn.UseVisualStyleBackColor = true;
            this.gamePathBtn.Click += new System.EventHandler(this.gamePathBtn_Click);
            this.gamePathBtn.MouseEnter += new System.EventHandler(this.editorUpdaterBtn_MouseEnter);
            // 
            // editorUpdaterBtn
            // 
            this.editorUpdaterBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.editorUpdaterBtn.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.editorUpdaterBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.editorUpdaterBtn.Location = new System.Drawing.Point(267, 214);
            this.editorUpdaterBtn.Margin = new System.Windows.Forms.Padding(4);
            this.editorUpdaterBtn.Name = "editorUpdaterBtn";
            this.editorUpdaterBtn.Size = new System.Drawing.Size(190, 38);
            this.editorUpdaterBtn.TabIndex = 41;
            this.editorUpdaterBtn.Text = "Manual Update";
            this.editorUpdaterBtn.UseVisualStyleBackColor = true;
            this.editorUpdaterBtn.Click += new System.EventHandler(this.editorUpdaterBtn_Click);
            this.editorUpdaterBtn.MouseEnter += new System.EventHandler(this.editorUpdaterBtn_MouseEnter);
            // 
            // gameSettingsLbl
            // 
            this.gameSettingsLbl.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gameSettingsLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gameSettingsLbl.Location = new System.Drawing.Point(511, 12);
            this.gameSettingsLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.gameSettingsLbl.Name = "gameSettingsLbl";
            this.gameSettingsLbl.Size = new System.Drawing.Size(398, 33);
            this.gameSettingsLbl.TabIndex = 40;
            this.gameSettingsLbl.Text = "Game Settings";
            this.gameSettingsLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // appSettingLbl
            // 
            this.appSettingLbl.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.appSettingLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.appSettingLbl.Location = new System.Drawing.Point(42, 12);
            this.appSettingLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.appSettingLbl.Name = "appSettingLbl";
            this.appSettingLbl.Size = new System.Drawing.Size(398, 33);
            this.appSettingLbl.TabIndex = 40;
            this.appSettingLbl.Text = "Application Settings";
            this.appSettingLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // configLoadBtn
            // 
            this.configLoadBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.configLoadBtn.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.configLoadBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.configLoadBtn.Location = new System.Drawing.Point(759, 260);
            this.configLoadBtn.Margin = new System.Windows.Forms.Padding(4);
            this.configLoadBtn.Name = "configLoadBtn";
            this.configLoadBtn.Size = new System.Drawing.Size(190, 38);
            this.configLoadBtn.TabIndex = 35;
            this.configLoadBtn.Text = "Config Load";
            this.configLoadBtn.UseVisualStyleBackColor = true;
            this.configLoadBtn.Click += new System.EventHandler(this.configLoadBtn_Click);
            // 
            // configSaveBtn
            // 
            this.configSaveBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.configSaveBtn.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.configSaveBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.configSaveBtn.Location = new System.Drawing.Point(759, 215);
            this.configSaveBtn.Margin = new System.Windows.Forms.Padding(4);
            this.configSaveBtn.Name = "configSaveBtn";
            this.configSaveBtn.Size = new System.Drawing.Size(190, 38);
            this.configSaveBtn.TabIndex = 34;
            this.configSaveBtn.Text = "Config Save";
            this.configSaveBtn.UseVisualStyleBackColor = true;
            this.configSaveBtn.Click += new System.EventHandler(this.configSaveBtn_Click);
            // 
            // updateIntervalTxtx
            // 
            this.updateIntervalTxtx.BackColor = System.Drawing.SystemColors.Control;
            this.updateIntervalTxtx.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.updateIntervalTxtx.ForeColor = System.Drawing.SystemColors.ControlText;
            this.updateIntervalTxtx.Location = new System.Drawing.Point(267, 177);
            this.updateIntervalTxtx.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.updateIntervalTxtx.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.updateIntervalTxtx.Name = "updateIntervalTxtx";
            this.updateIntervalTxtx.Size = new System.Drawing.Size(63, 30);
            this.updateIntervalTxtx.TabIndex = 42;
            this.updateIntervalTxtx.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.updateIntervalTxtx.ValueChanged += new System.EventHandler(this.autoUpdateTimeTxt_TextChanged);
            // 
            // musicTrackBar
            // 
            this.musicTrackBar.AutoSize = false;
            this.musicTrackBar.Location = new System.Drawing.Point(958, 96);
            this.musicTrackBar.Name = "musicTrackBar";
            this.musicTrackBar.Size = new System.Drawing.Size(171, 37);
            this.musicTrackBar.TabIndex = 33;
            // 
            // musicSoundCb
            // 
            this.musicSoundCb.AutoSize = true;
            this.musicSoundCb.Checked = true;
            this.musicSoundCb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.musicSoundCb.Font = new System.Drawing.Font("Century Gothic", 8F);
            this.musicSoundCb.Location = new System.Drawing.Point(975, 138);
            this.musicSoundCb.Margin = new System.Windows.Forms.Padding(4);
            this.musicSoundCb.Name = "musicSoundCb";
            this.musicSoundCb.Size = new System.Drawing.Size(70, 23);
            this.musicSoundCb.TabIndex = 32;
            this.musicSoundCb.Text = "Music";
            this.musicSoundCb.UseVisualStyleBackColor = true;
            this.musicSoundCb.CheckedChanged += new System.EventHandler(this.musicSoundCb_CheckedChanged);
            // 
            // sfxMusicCb
            // 
            this.sfxMusicCb.AutoSize = true;
            this.sfxMusicCb.Font = new System.Drawing.Font("Century Gothic", 8F);
            this.sfxMusicCb.Location = new System.Drawing.Point(1053, 138);
            this.sfxMusicCb.Margin = new System.Windows.Forms.Padding(4);
            this.sfxMusicCb.Name = "sfxMusicCb";
            this.sfxMusicCb.Size = new System.Drawing.Size(54, 23);
            this.sfxMusicCb.TabIndex = 32;
            this.sfxMusicCb.Text = "SFX";
            this.sfxMusicCb.UseVisualStyleBackColor = true;
            this.sfxMusicCb.CheckedChanged += new System.EventHandler(this.sfxMusicCb_CheckedChanged);
            // 
            // enableMusicCb
            // 
            this.enableMusicCb.AutoSize = true;
            this.enableMusicCb.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.enableMusicCb.Location = new System.Drawing.Point(539, 49);
            this.enableMusicCb.Margin = new System.Windows.Forms.Padding(4);
            this.enableMusicCb.Name = "enableMusicCb";
            this.enableMusicCb.Size = new System.Drawing.Size(142, 25);
            this.enableMusicCb.TabIndex = 32;
            this.enableMusicCb.Text = "Music Enable";
            this.enableMusicCb.UseVisualStyleBackColor = true;
            this.enableMusicCb.CheckedChanged += new System.EventHandler(this.enableMusicCb_CheckedChanged);
            // 
            // debugModeCb
            // 
            this.debugModeCb.AutoSize = true;
            this.debugModeCb.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.debugModeCb.Location = new System.Drawing.Point(539, 106);
            this.debugModeCb.Margin = new System.Windows.Forms.Padding(4);
            this.debugModeCb.Name = "debugModeCb";
            this.debugModeCb.Size = new System.Drawing.Size(145, 25);
            this.debugModeCb.TabIndex = 32;
            this.debugModeCb.Text = "Debug Mode";
            this.debugModeCb.UseVisualStyleBackColor = true;
            this.debugModeCb.CheckedChanged += new System.EventHandler(this.debugModeCb_CheckedChanged);
            // 
            // udpateMusicBtn
            // 
            this.udpateMusicBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.udpateMusicBtn.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.udpateMusicBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.udpateMusicBtn.Location = new System.Drawing.Point(759, 95);
            this.udpateMusicBtn.Margin = new System.Windows.Forms.Padding(4);
            this.udpateMusicBtn.Name = "udpateMusicBtn";
            this.udpateMusicBtn.Size = new System.Drawing.Size(190, 38);
            this.udpateMusicBtn.TabIndex = 25;
            this.udpateMusicBtn.Text = "Set Music";
            this.udpateMusicBtn.UseVisualStyleBackColor = true;
            this.udpateMusicBtn.Click += new System.EventHandler(this.udpateMusicBtn_Click);
            // 
            // gfxResetBtn
            // 
            this.gfxResetBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gfxResetBtn.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gfxResetBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gfxResetBtn.Location = new System.Drawing.Point(759, 170);
            this.gfxResetBtn.Margin = new System.Windows.Forms.Padding(4);
            this.gfxResetBtn.Name = "gfxResetBtn";
            this.gfxResetBtn.Size = new System.Drawing.Size(190, 38);
            this.gfxResetBtn.TabIndex = 25;
            this.gfxResetBtn.Text = "Graphics Reset";
            this.gfxResetBtn.UseVisualStyleBackColor = true;
            this.gfxResetBtn.Click += new System.EventHandler(this.gfxResetBtn_Click);
            // 
            // setFramesBtn
            // 
            this.setFramesBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.setFramesBtn.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.setFramesBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.setFramesBtn.Location = new System.Drawing.Point(759, 50);
            this.setFramesBtn.Margin = new System.Windows.Forms.Padding(4);
            this.setFramesBtn.Name = "setFramesBtn";
            this.setFramesBtn.Size = new System.Drawing.Size(190, 38);
            this.setFramesBtn.TabIndex = 25;
            this.setFramesBtn.Text = "Set Frames";
            this.setFramesBtn.UseVisualStyleBackColor = true;
            this.setFramesBtn.Click += new System.EventHandler(this.setFramesBtn_Click);
            // 
            // aiIdleCb
            // 
            this.aiIdleCb.AutoSize = true;
            this.aiIdleCb.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.aiIdleCb.Location = new System.Drawing.Point(539, 74);
            this.aiIdleCb.Margin = new System.Windows.Forms.Padding(4);
            this.aiIdleCb.Name = "aiIdleCb";
            this.aiIdleCb.Size = new System.Drawing.Size(147, 25);
            this.aiIdleCb.TabIndex = 24;
            this.aiIdleCb.Text = "A.I Idle Mode";
            this.aiIdleCb.UseVisualStyleBackColor = true;
            this.aiIdleCb.CheckedChanged += new System.EventHandler(this.aiIdleCb_CheckedChanged);
            // 
            // appSupportBtn
            // 
            this.appSupportBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.appSupportBtn.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.appSupportBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.appSupportBtn.Location = new System.Drawing.Point(42, 307);
            this.appSupportBtn.Margin = new System.Windows.Forms.Padding(4);
            this.appSupportBtn.Name = "appSupportBtn";
            this.appSupportBtn.Size = new System.Drawing.Size(190, 38);
            this.appSupportBtn.TabIndex = 23;
            this.appSupportBtn.Text = "App Support";
            this.appSupportBtn.UseVisualStyleBackColor = true;
            this.appSupportBtn.Click += new System.EventHandler(this.appSupportBtn_Click);
            // 
            // disableWarningsCb
            // 
            this.disableWarningsCb.AutoSize = true;
            this.disableWarningsCb.Checked = true;
            this.disableWarningsCb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.disableWarningsCb.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.disableWarningsCb.Location = new System.Drawing.Point(539, 138);
            this.disableWarningsCb.Margin = new System.Windows.Forms.Padding(4);
            this.disableWarningsCb.Name = "disableWarningsCb";
            this.disableWarningsCb.Size = new System.Drawing.Size(166, 25);
            this.disableWarningsCb.TabIndex = 22;
            this.disableWarningsCb.Text = "Disable Warning";
            this.disableWarningsCb.UseVisualStyleBackColor = true;
            this.disableWarningsCb.CheckedChanged += new System.EventHandler(this.disableWarningsCb_CheckedChanged);
            // 
            // showAppLogBtn
            // 
            this.showAppLogBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.showAppLogBtn.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.showAppLogBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.showAppLogBtn.Location = new System.Drawing.Point(42, 216);
            this.showAppLogBtn.Margin = new System.Windows.Forms.Padding(4);
            this.showAppLogBtn.Name = "showAppLogBtn";
            this.showAppLogBtn.Size = new System.Drawing.Size(112, 38);
            this.showAppLogBtn.TabIndex = 21;
            this.showAppLogBtn.Text = "App Logs";
            this.showAppLogBtn.UseVisualStyleBackColor = true;
            this.showAppLogBtn.Click += new System.EventHandler(this.showAppLogBtn_Click);
            // 
            // xmlCb
            // 
            this.xmlCb.AutoSize = true;
            this.xmlCb.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xmlCb.Location = new System.Drawing.Point(190, 96);
            this.xmlCb.Margin = new System.Windows.Forms.Padding(4);
            this.xmlCb.Name = "xmlCb";
            this.xmlCb.Size = new System.Drawing.Size(62, 24);
            this.xmlCb.TabIndex = 20;
            this.xmlCb.Text = "XML";
            this.xmlCb.UseVisualStyleBackColor = true;
            this.xmlCb.CheckedChanged += new System.EventHandler(this.xmlCb_CheckedChanged);
            // 
            // jsonCb
            // 
            this.jsonCb.AutoSize = true;
            this.jsonCb.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.jsonCb.Location = new System.Drawing.Point(113, 96);
            this.jsonCb.Margin = new System.Windows.Forms.Padding(4);
            this.jsonCb.Name = "jsonCb";
            this.jsonCb.Size = new System.Drawing.Size(69, 24);
            this.jsonCb.TabIndex = 20;
            this.jsonCb.Text = "JSON";
            this.jsonCb.UseVisualStyleBackColor = true;
            this.jsonCb.CheckedChanged += new System.EventHandler(this.jsonCb_CheckedChanged);
            // 
            // csvCb
            // 
            this.csvCb.AutoSize = true;
            this.csvCb.Checked = true;
            this.csvCb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.csvCb.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.csvCb.Location = new System.Drawing.Point(42, 96);
            this.csvCb.Margin = new System.Windows.Forms.Padding(4);
            this.csvCb.Name = "csvCb";
            this.csvCb.Size = new System.Drawing.Size(61, 24);
            this.csvCb.TabIndex = 20;
            this.csvCb.Text = "CSV";
            this.csvCb.UseVisualStyleBackColor = true;
            this.csvCb.CheckedChanged += new System.EventHandler(this.csvCb_CheckedChanged);
            // 
            // updateCheckerCb
            // 
            this.updateCheckerCb.AutoSize = true;
            this.updateCheckerCb.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.updateCheckerCb.Location = new System.Drawing.Point(267, 146);
            this.updateCheckerCb.Margin = new System.Windows.Forms.Padding(4);
            this.updateCheckerCb.Name = "updateCheckerCb";
            this.updateCheckerCb.Size = new System.Drawing.Size(194, 25);
            this.updateCheckerCb.TabIndex = 20;
            this.updateCheckerCb.Text = "Check For Updates";
            this.updateCheckerCb.UseVisualStyleBackColor = true;
            this.updateCheckerCb.CheckedChanged += new System.EventHandler(this.updateCheckerCb_CheckedChanged);
            this.updateCheckerCb.MouseEnter += new System.EventHandler(this.updateCheckerCb_MouseEnter);
            // 
            // appLogsCb
            // 
            this.appLogsCb.AutoSize = true;
            this.appLogsCb.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.appLogsCb.Location = new System.Drawing.Point(267, 103);
            this.appLogsCb.Margin = new System.Windows.Forms.Padding(4);
            this.appLogsCb.Name = "appLogsCb";
            this.appLogsCb.Size = new System.Drawing.Size(166, 25);
            this.appLogsCb.TabIndex = 20;
            this.appLogsCb.Text = "Application logs";
            this.appLogsCb.UseVisualStyleBackColor = true;
            this.appLogsCb.CheckedChanged += new System.EventHandler(this.appLogsCb_CheckedChanged);
            // 
            // autoRefreshGameCb
            // 
            this.autoRefreshGameCb.AutoSize = true;
            this.autoRefreshGameCb.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.autoRefreshGameCb.Location = new System.Drawing.Point(267, 75);
            this.autoRefreshGameCb.Margin = new System.Windows.Forms.Padding(4);
            this.autoRefreshGameCb.Name = "autoRefreshGameCb";
            this.autoRefreshGameCb.Size = new System.Drawing.Size(186, 25);
            this.autoRefreshGameCb.TabIndex = 19;
            this.autoRefreshGameCb.Text = "Auto refresh game";
            this.autoRefreshGameCb.UseVisualStyleBackColor = true;
            this.autoRefreshGameCb.CheckedChanged += new System.EventHandler(this.autoRefreshGameCb_CheckedChanged);
            // 
            // autoResetCb
            // 
            this.autoResetCb.AutoSize = true;
            this.autoResetCb.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.autoResetCb.Location = new System.Drawing.Point(267, 50);
            this.autoResetCb.Margin = new System.Windows.Forms.Padding(4);
            this.autoResetCb.Name = "autoResetCb";
            this.autoResetCb.Size = new System.Drawing.Size(163, 25);
            this.autoResetCb.TabIndex = 19;
            this.autoResetCb.Text = "Auto reset level";
            this.autoResetCb.UseVisualStyleBackColor = true;
            this.autoResetCb.CheckedChanged += new System.EventHandler(this.autoResetCb_CheckedChanged);
            // 
            // cutsceneRemoveBtn
            // 
            this.cutsceneRemoveBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cutsceneRemoveBtn.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.cutsceneRemoveBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cutsceneRemoveBtn.Location = new System.Drawing.Point(542, 260);
            this.cutsceneRemoveBtn.Margin = new System.Windows.Forms.Padding(4);
            this.cutsceneRemoveBtn.Name = "cutsceneRemoveBtn";
            this.cutsceneRemoveBtn.Size = new System.Drawing.Size(190, 38);
            this.cutsceneRemoveBtn.TabIndex = 18;
            this.cutsceneRemoveBtn.Text = "Remove Cutscenes";
            this.cutsceneRemoveBtn.UseVisualStyleBackColor = true;
            this.cutsceneRemoveBtn.Click += new System.EventHandler(this.cutsceneRemoveBtn_Click);
            // 
            // resetScriptsFileBtn
            // 
            this.resetScriptsFileBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resetScriptsFileBtn.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resetScriptsFileBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.resetScriptsFileBtn.Location = new System.Drawing.Point(542, 215);
            this.resetScriptsFileBtn.Margin = new System.Windows.Forms.Padding(4);
            this.resetScriptsFileBtn.Name = "resetScriptsFileBtn";
            this.resetScriptsFileBtn.Size = new System.Drawing.Size(190, 38);
            this.resetScriptsFileBtn.TabIndex = 18;
            this.resetScriptsFileBtn.Text = "Reset File";
            this.resetScriptsFileBtn.UseVisualStyleBackColor = true;
            this.resetScriptsFileBtn.Click += new System.EventHandler(this.resetScriptsFileBtn_Click);
            // 
            // resetLevelBtn
            // 
            this.resetLevelBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resetLevelBtn.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resetLevelBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.resetLevelBtn.Location = new System.Drawing.Point(542, 170);
            this.resetLevelBtn.Margin = new System.Windows.Forms.Padding(4);
            this.resetLevelBtn.Name = "resetLevelBtn";
            this.resetLevelBtn.Size = new System.Drawing.Size(190, 38);
            this.resetLevelBtn.TabIndex = 18;
            this.resetLevelBtn.Text = "Reset Level";
            this.resetLevelBtn.UseVisualStyleBackColor = true;
            this.resetLevelBtn.Click += new System.EventHandler(this.resetLevelBtn_Click);
            // 
            // clearCacheBtn
            // 
            this.clearCacheBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clearCacheBtn.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.clearCacheBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.clearCacheBtn.Location = new System.Drawing.Point(42, 170);
            this.clearCacheBtn.Margin = new System.Windows.Forms.Padding(4);
            this.clearCacheBtn.Name = "clearCacheBtn";
            this.clearCacheBtn.Size = new System.Drawing.Size(190, 38);
            this.clearCacheBtn.TabIndex = 16;
            this.clearCacheBtn.Text = "Clear Cache";
            this.clearCacheBtn.UseVisualStyleBackColor = true;
            this.clearCacheBtn.Click += new System.EventHandler(this.clearCacheBtn_Click);
            // 
            // exportObjectsBtn
            // 
            this.exportObjectsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exportObjectsBtn.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.exportObjectsBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.exportObjectsBtn.Location = new System.Drawing.Point(42, 50);
            this.exportObjectsBtn.Margin = new System.Windows.Forms.Padding(4);
            this.exportObjectsBtn.Name = "exportObjectsBtn";
            this.exportObjectsBtn.Size = new System.Drawing.Size(193, 38);
            this.exportObjectsBtn.TabIndex = 16;
            this.exportObjectsBtn.Text = "Export objects";
            this.exportObjectsBtn.UseVisualStyleBackColor = true;
            this.exportObjectsBtn.Click += new System.EventHandler(this.exportObjectsBtn_Click);
            // 
            // devMode
            // 
            this.devMode.BackColor = System.Drawing.SystemColors.Control;
            this.devMode.Controls.Add(this.devAutoFormatCb);
            this.devMode.Controls.Add(this.devClearContentsCb);
            this.devMode.Controls.Add(this.devViewerTxt);
            this.devMode.Controls.Add(this.saveDevFileBtn);
            this.devMode.Controls.Add(this.loadDevFileBtn);
            this.devMode.Controls.Add(this.uploadUpdateBtn);
            this.devMode.Controls.Add(this.devFileNameTxt);
            this.devMode.Controls.Add(this.devFileSizeTxt);
            this.devMode.Controls.Add(this.devVersionTxt);
            this.devMode.Controls.Add(this.createUpdateBtn);
            this.devMode.ForeColor = System.Drawing.SystemColors.ControlText;
            this.devMode.Location = new System.Drawing.Point(4, 30);
            this.devMode.Name = "devMode";
            this.devMode.Size = new System.Drawing.Size(1137, 387);
            this.devMode.TabIndex = 10;
            this.devMode.Text = "Dev Mode";
            // 
            // devAutoFormatCb
            // 
            this.devAutoFormatCb.AutoSize = true;
            this.devAutoFormatCb.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.devAutoFormatCb.Location = new System.Drawing.Point(985, 248);
            this.devAutoFormatCb.Margin = new System.Windows.Forms.Padding(4);
            this.devAutoFormatCb.Name = "devAutoFormatCb";
            this.devAutoFormatCb.Size = new System.Drawing.Size(115, 23);
            this.devAutoFormatCb.TabIndex = 55;
            this.devAutoFormatCb.Text = "Auto Format";
            this.devAutoFormatCb.UseVisualStyleBackColor = true;
            // 
            // devClearContentsCb
            // 
            this.devClearContentsCb.AutoSize = true;
            this.devClearContentsCb.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.devClearContentsCb.Location = new System.Drawing.Point(879, 248);
            this.devClearContentsCb.Margin = new System.Windows.Forms.Padding(4);
            this.devClearContentsCb.Name = "devClearContentsCb";
            this.devClearContentsCb.Size = new System.Drawing.Size(107, 23);
            this.devClearContentsCb.TabIndex = 55;
            this.devClearContentsCb.Text = "Clear Data";
            this.devClearContentsCb.UseVisualStyleBackColor = true;
            // 
            // devViewerTxt
            // 
            this.devViewerTxt.AutoWordSelection = true;
            this.devViewerTxt.BackColor = System.Drawing.SystemColors.Control;
            this.devViewerTxt.ForeColor = System.Drawing.Color.White;
            this.devViewerTxt.Location = new System.Drawing.Point(7, 4);
            this.devViewerTxt.Name = "devViewerTxt";
            this.devViewerTxt.Size = new System.Drawing.Size(865, 383);
            this.devViewerTxt.TabIndex = 54;
            this.devViewerTxt.Text = "DEVELOPER TEXT VIEWER";
            this.devViewerTxt.TextChanged += new System.EventHandler(this.devViewerTxt_TextChanged);
            this.devViewerTxt.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.devViewerTxt_PreviewKeyDown);
            // 
            // saveDevFileBtn
            // 
            this.saveDevFileBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveDevFileBtn.Font = new System.Drawing.Font("Century Gothic", 10.2F);
            this.saveDevFileBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.saveDevFileBtn.Location = new System.Drawing.Point(879, 199);
            this.saveDevFileBtn.Margin = new System.Windows.Forms.Padding(4);
            this.saveDevFileBtn.Name = "saveDevFileBtn";
            this.saveDevFileBtn.Size = new System.Drawing.Size(254, 41);
            this.saveDevFileBtn.TabIndex = 53;
            this.saveDevFileBtn.Text = "Save File";
            this.saveDevFileBtn.UseVisualStyleBackColor = true;
            this.saveDevFileBtn.Click += new System.EventHandler(this.saveDevFileBtn_Click);
            // 
            // loadDevFileBtn
            // 
            this.loadDevFileBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loadDevFileBtn.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadDevFileBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.loadDevFileBtn.Location = new System.Drawing.Point(879, 150);
            this.loadDevFileBtn.Margin = new System.Windows.Forms.Padding(4);
            this.loadDevFileBtn.Name = "loadDevFileBtn";
            this.loadDevFileBtn.Size = new System.Drawing.Size(254, 41);
            this.loadDevFileBtn.TabIndex = 53;
            this.loadDevFileBtn.Text = "Load File";
            this.loadDevFileBtn.UseVisualStyleBackColor = true;
            this.loadDevFileBtn.Click += new System.EventHandler(this.loadDevFileBtn_Click);
            // 
            // uploadUpdateBtn
            // 
            this.uploadUpdateBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.uploadUpdateBtn.Font = new System.Drawing.Font("Century Gothic", 10.2F);
            this.uploadUpdateBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.uploadUpdateBtn.Location = new System.Drawing.Point(879, 342);
            this.uploadUpdateBtn.Margin = new System.Windows.Forms.Padding(4);
            this.uploadUpdateBtn.Name = "uploadUpdateBtn";
            this.uploadUpdateBtn.Size = new System.Drawing.Size(254, 41);
            this.uploadUpdateBtn.TabIndex = 53;
            this.uploadUpdateBtn.Text = "Upload Update";
            this.uploadUpdateBtn.UseVisualStyleBackColor = true;
            this.uploadUpdateBtn.Click += new System.EventHandler(this.uploadUpdateBtn_Click);
            // 
            // devFileNameTxt
            // 
            this.devFileNameTxt.BackColor = System.Drawing.SystemColors.Control;
            this.devFileNameTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.devFileNameTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.devFileNameTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.devFileNameTxt.Location = new System.Drawing.Point(887, 42);
            this.devFileNameTxt.Margin = new System.Windows.Forms.Padding(4);
            this.devFileNameTxt.Name = "devFileNameTxt";
            this.devFileNameTxt.ReadOnly = true;
            this.devFileNameTxt.Size = new System.Drawing.Size(223, 30);
            this.devFileNameTxt.TabIndex = 52;
            // 
            // devFileSizeTxt
            // 
            this.devFileSizeTxt.BackColor = System.Drawing.SystemColors.Control;
            this.devFileSizeTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.devFileSizeTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.devFileSizeTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.devFileSizeTxt.Location = new System.Drawing.Point(887, 80);
            this.devFileSizeTxt.Margin = new System.Windows.Forms.Padding(4);
            this.devFileSizeTxt.Name = "devFileSizeTxt";
            this.devFileSizeTxt.ReadOnly = true;
            this.devFileSizeTxt.Size = new System.Drawing.Size(223, 30);
            this.devFileSizeTxt.TabIndex = 52;
            // 
            // devVersionTxt
            // 
            this.devVersionTxt.BackColor = System.Drawing.SystemColors.Control;
            this.devVersionTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.devVersionTxt.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.devVersionTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.devVersionTxt.Location = new System.Drawing.Point(887, 4);
            this.devVersionTxt.Margin = new System.Windows.Forms.Padding(4);
            this.devVersionTxt.Name = "devVersionTxt";
            this.devVersionTxt.Size = new System.Drawing.Size(223, 30);
            this.devVersionTxt.TabIndex = 52;
            this.devVersionTxt.Text = "0.4.0.0";
            // 
            // createUpdateBtn
            // 
            this.createUpdateBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.createUpdateBtn.Font = new System.Drawing.Font("Century Gothic", 10.2F);
            this.createUpdateBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.createUpdateBtn.Location = new System.Drawing.Point(879, 293);
            this.createUpdateBtn.Margin = new System.Windows.Forms.Padding(4);
            this.createUpdateBtn.Name = "createUpdateBtn";
            this.createUpdateBtn.Size = new System.Drawing.Size(254, 41);
            this.createUpdateBtn.TabIndex = 50;
            this.createUpdateBtn.Text = "Create Update";
            this.createUpdateBtn.UseVisualStyleBackColor = true;
            this.createUpdateBtn.Click += new System.EventHandler(this.createUpdateBtn_Click);
            // 
            // levelNameLbl
            // 
            this.levelNameLbl.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.levelNameLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.levelNameLbl.Location = new System.Drawing.Point(11, 34);
            this.levelNameLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.levelNameLbl.Name = "levelNameLbl";
            this.levelNameLbl.Size = new System.Drawing.Size(200, 33);
            this.levelNameLbl.TabIndex = 2;
            this.levelNameLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // levelImgBox
            // 
            this.levelImgBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.levelImgBox.Location = new System.Drawing.Point(12, 71);
            this.levelImgBox.Margin = new System.Windows.Forms.Padding(4);
            this.levelImgBox.Name = "levelImgBox";
            this.levelImgBox.Size = new System.Drawing.Size(199, 184);
            this.levelImgBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.levelImgBox.TabIndex = 1;
            this.levelImgBox.TabStop = false;
            // 
            // appMenuSettings
            // 
            this.appMenuSettings.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.appMenuSettings.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.appSettingsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.appMenuSettings.Location = new System.Drawing.Point(0, 0);
            this.appMenuSettings.Name = "appMenuSettings";
            this.appMenuSettings.Size = new System.Drawing.Size(1145, 28);
            this.appMenuSettings.TabIndex = 49;
            this.appMenuSettings.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editorOnlineToolStripMenuItem,
            this.selectEditorModeToolStripMenuItem,
            this.editorConnectionToolStripMenuItem,
            this.startGameToolStripMenuItem,
            this.gameStatusToolStripMenuItem,
            this.compilerToolStripMenuItem,
            this.positionToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(64, 24);
            this.settingsToolStripMenuItem.Text = " Menu";
            // 
            // editorOnlineToolStripMenuItem
            // 
            this.editorOnlineToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startGameBtnMenu,
            this.restartLevelBtnMenu,
            this.quitLevelBtnMenu,
            this.refreshGameBtnMenu});
            this.editorOnlineToolStripMenuItem.Name = "editorOnlineToolStripMenuItem";
            this.editorOnlineToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.editorOnlineToolStripMenuItem.Text = "Level Menu";
            // 
            // startGameBtnMenu
            // 
            this.startGameBtnMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.levelStartTxtMenu});
            this.startGameBtnMenu.Name = "startGameBtnMenu";
            this.startGameBtnMenu.Size = new System.Drawing.Size(224, 26);
            this.startGameBtnMenu.Text = "Start";
            this.startGameBtnMenu.Click += new System.EventHandler(this.startGameBtnMenu_Click);
            // 
            // levelStartTxtMenu
            // 
            this.levelStartTxtMenu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.levelStartTxtMenu.Name = "levelStartTxtMenu";
            this.levelStartTxtMenu.Size = new System.Drawing.Size(224, 27);
            this.levelStartTxtMenu.Text = "1";
            // 
            // restartLevelBtnMenu
            // 
            this.restartLevelBtnMenu.Name = "restartLevelBtnMenu";
            this.restartLevelBtnMenu.Size = new System.Drawing.Size(224, 26);
            this.restartLevelBtnMenu.Text = "Restart";
            this.restartLevelBtnMenu.Click += new System.EventHandler(this.restartLevelBtn_Click);
            // 
            // quitLevelBtnMenu
            // 
            this.quitLevelBtnMenu.Name = "quitLevelBtnMenu";
            this.quitLevelBtnMenu.Size = new System.Drawing.Size(224, 26);
            this.quitLevelBtnMenu.Text = "Quit";
            this.quitLevelBtnMenu.Click += new System.EventHandler(this.quitLevelBtn_Click);
            // 
            // refreshGameBtnMenu
            // 
            this.refreshGameBtnMenu.Name = "refreshGameBtnMenu";
            this.refreshGameBtnMenu.Size = new System.Drawing.Size(224, 26);
            this.refreshGameBtnMenu.Text = "Refresh";
            this.refreshGameBtnMenu.Click += new System.EventHandler(this.refreshGame_Click);
            // 
            // selectEditorModeToolStripMenuItem
            // 
            this.selectEditorModeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playModeCb,
            this.editorModeCb,
            this.liveEditorCb});
            this.selectEditorModeToolStripMenuItem.Name = "selectEditorModeToolStripMenuItem";
            this.selectEditorModeToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.selectEditorModeToolStripMenuItem.Text = "Editor Mode";
            // 
            // playModeCb
            // 
            this.playModeCb.Name = "playModeCb";
            this.playModeCb.Size = new System.Drawing.Size(224, 26);
            this.playModeCb.Text = "Play Mode";
            this.playModeCb.Click += new System.EventHandler(this.playModeCb_Click);
            // 
            // editorModeCb
            // 
            this.editorModeCb.Name = "editorModeCb";
            this.editorModeCb.Size = new System.Drawing.Size(224, 26);
            this.editorModeCb.Text = "Edit Mode";
            this.editorModeCb.Click += new System.EventHandler(this.editorModeCb_Click);
            // 
            // liveEditorCb
            // 
            this.liveEditorCb.Name = "liveEditorCb";
            this.liveEditorCb.Size = new System.Drawing.Size(224, 26);
            this.liveEditorCb.Text = "Live Mode";
            this.liveEditorCb.Click += new System.EventHandler(this.liveEditorCb_Click);
            // 
            // editorConnectionToolStripMenuItem
            // 
            this.editorConnectionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editorOnlineCb});
            this.editorConnectionToolStripMenuItem.Name = "editorConnectionToolStripMenuItem";
            this.editorConnectionToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.editorConnectionToolStripMenuItem.Text = "Editor Connection";
            // 
            // editorOnlineCb
            // 
            this.editorOnlineCb.Checked = true;
            this.editorOnlineCb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.editorOnlineCb.Name = "editorOnlineCb";
            this.editorOnlineCb.Size = new System.Drawing.Size(224, 26);
            this.editorOnlineCb.Text = "Online";
            this.editorOnlineCb.Click += new System.EventHandler(this.editorOnlineCb_Click);
            // 
            // startGameToolStripMenuItem
            // 
            this.startGameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startWindowedGameBtn,
            this.startFullScreenGameBtn});
            this.startGameToolStripMenuItem.Name = "startGameToolStripMenuItem";
            this.startGameToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.startGameToolStripMenuItem.Text = "Start Game";
            // 
            // startWindowedGameBtn
            // 
            this.startWindowedGameBtn.Name = "startWindowedGameBtn";
            this.startWindowedGameBtn.Size = new System.Drawing.Size(163, 26);
            this.startWindowedGameBtn.Text = "Window";
            this.startWindowedGameBtn.Click += new System.EventHandler(this.startWindowedGameBtn_Click);
            // 
            // startFullScreenGameBtn
            // 
            this.startFullScreenGameBtn.Name = "startFullScreenGameBtn";
            this.startFullScreenGameBtn.Size = new System.Drawing.Size(163, 26);
            this.startFullScreenGameBtn.Text = "Full Screen";
            this.startFullScreenGameBtn.Click += new System.EventHandler(this.startFullScreenGameBtn_Click);
            // 
            // gameStatusToolStripMenuItem
            // 
            this.gameStatusToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.internalsStatusMenu,
            this.profileToolStripMenuItem});
            this.gameStatusToolStripMenuItem.Name = "gameStatusToolStripMenuItem";
            this.gameStatusToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.gameStatusToolStripMenuItem.Text = "Game Status";
            // 
            // internalsStatusMenu
            // 
            this.internalsStatusMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.internalsStatusLbl});
            this.internalsStatusMenu.Name = "internalsStatusMenu";
            this.internalsStatusMenu.Size = new System.Drawing.Size(148, 26);
            this.internalsStatusMenu.Text = "Internals";
            // 
            // internalsStatusLbl
            // 
            this.internalsStatusLbl.Name = "internalsStatusLbl";
            this.internalsStatusLbl.Size = new System.Drawing.Size(119, 26);
            this.internalsStatusLbl.Text = "N/A";
            // 
            // profileToolStripMenuItem
            // 
            this.profileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameProfileNameLbl,
            this.gameProfileMissionLbl});
            this.profileToolStripMenuItem.Name = "profileToolStripMenuItem";
            this.profileToolStripMenuItem.Size = new System.Drawing.Size(148, 26);
            this.profileToolStripMenuItem.Text = "Profile";
            // 
            // gameProfileNameLbl
            // 
            this.gameProfileNameLbl.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gameProfileNameLbl.Name = "gameProfileNameLbl";
            this.gameProfileNameLbl.Size = new System.Drawing.Size(224, 27);
            this.gameProfileNameLbl.Text = "N/A";
            // 
            // gameProfileMissionLbl
            // 
            this.gameProfileMissionLbl.Name = "gameProfileMissionLbl";
            this.gameProfileMissionLbl.Size = new System.Drawing.Size(298, 26);
            this.gameProfileMissionLbl.Text = "N/A";
            // 
            // compilerToolStripMenuItem
            // 
            this.compilerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.compilerTypeLbl});
            this.compilerToolStripMenuItem.Name = "compilerToolStripMenuItem";
            this.compilerToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.compilerToolStripMenuItem.Text = "Compiler";
            // 
            // compilerTypeLbl
            // 
            this.compilerTypeLbl.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.compilerTypeLbl.Name = "compilerTypeLbl";
            this.compilerTypeLbl.Size = new System.Drawing.Size(224, 27);
            this.compilerTypeLbl.Text = "Internal";
            // 
            // positionToolStripMenuItem
            // 
            this.positionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.posCoordCb,
            this.posMetersCb});
            this.positionToolStripMenuItem.Name = "positionToolStripMenuItem";
            this.positionToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.positionToolStripMenuItem.Text = "Position";
            // 
            // posCoordCb
            // 
            this.posCoordCb.Name = "posCoordCb";
            this.posCoordCb.Size = new System.Drawing.Size(224, 26);
            this.posCoordCb.Text = "Coordinates";
            this.posCoordCb.Click += new System.EventHandler(this.posCoordCb_Click);
            // 
            // posMetersCb
            // 
            this.posMetersCb.Name = "posMetersCb";
            this.posMetersCb.Size = new System.Drawing.Size(224, 26);
            this.posMetersCb.Text = "Meter";
            this.posMetersCb.Click += new System.EventHandler(this.posMetersCb_Click);
            // 
            // appSettingsToolStripMenuItem
            // 
            this.appSettingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameSettingsToolStripMenuItem,
            this.applicationSettingsToolStripMenuItem});
            this.appSettingsToolStripMenuItem.Name = "appSettingsToolStripMenuItem";
            this.appSettingsToolStripMenuItem.Size = new System.Drawing.Size(76, 24);
            this.appSettingsToolStripMenuItem.Text = "Settings";
            // 
            // gameSettingsToolStripMenuItem
            // 
            this.gameSettingsToolStripMenuItem.Name = "gameSettingsToolStripMenuItem";
            this.gameSettingsToolStripMenuItem.Size = new System.Drawing.Size(226, 26);
            this.gameSettingsToolStripMenuItem.Text = "Game Settings";
            // 
            // applicationSettingsToolStripMenuItem
            // 
            this.applicationSettingsToolStripMenuItem.Name = "applicationSettingsToolStripMenuItem";
            this.applicationSettingsToolStripMenuItem.Size = new System.Drawing.Size(226, 26);
            this.applicationSettingsToolStripMenuItem.Text = "Application Settings";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.versionToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(140, 26);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // versionToolStripMenuItem
            // 
            this.versionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.versionLbl});
            this.versionToolStripMenuItem.Name = "versionToolStripMenuItem";
            this.versionToolStripMenuItem.Size = new System.Drawing.Size(140, 26);
            this.versionToolStripMenuItem.Text = "Version";
            // 
            // versionLbl
            // 
            this.versionLbl.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.versionLbl.Name = "versionLbl";
            this.versionLbl.Size = new System.Drawing.Size(224, 27);
            this.versionLbl.Text = "rw";
            // 
            // IGIEditorUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1147, 741);
            this.Controls.Add(this.editorMainPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.appMenuSettings;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "IGIEditorUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IGI 1 Editor ";
            this.editorMainPanel.ResumeLayout(false);
            this.editorMainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.levelStartTxt)).EndInit();
            this.editorTabs.ResumeLayout(false);
            this.levelEditor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.objectImgBox)).EndInit();
            this.objectEditor.ResumeLayout(false);
            this.objectEditor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buildingsResetTxt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectsResetTxt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buildingsRemoveTxt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectsRemoveTxt)).EndInit();
            this.humanEditor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.inAirSpeedTxt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upwardJumpTxt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.forwardJumpTxt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.peekTimeTxt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.peekCrouchTxt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.peekLRTxt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.humanViewCamTxt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.damageScaleFallTxt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.damageScaleFenceTxt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teamIdTxt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.damageScaleTxt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.movementSpeedTxt)).EndInit();
            this.aiEditor.ResumeLayout(false);
            this.aiEditorTabs.ResumeLayout(false);
            this.aiEditorMainTab.ResumeLayout(false);
            this.aiEditorMainTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teamIdText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxSpawnsTxt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aiCountTxt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.weaponAIImgBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aiImgBox)).EndInit();
            this.aiJSONEditor.ResumeLayout(false);
            this.aiJSONEditor.PerformLayout();
            this.weaponEditor.ResumeLayout(false);
            this.weaponEditorTabs.ResumeLayout(false);
            this.weaponEditorMainTab.ResumeLayout(false);
            this.weaponEditorMainTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.weaponAmmoTxt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.weaponImgBox)).EndInit();
            this.weaponCfgEditor.ResumeLayout(false);
            this.weaponCfgEditor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.weaponRangeTxt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.weaponRoundPerMinuteTxt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.weaponBulletsTxt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.weaponPowerTxt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.weaponDamageTxt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.weaponRoundPerClipTxt)).EndInit();
            this.threeDEditor.ResumeLayout(false);
            this.missionEditor.ResumeLayout(false);
            this.missionEditor.PerformLayout();
            this.graphEditor.ResumeLayout(false);
            this.graphEditor.PerformLayout();
            this.positionEditor.ResumeLayout(false);
            this.positionEditor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gammaTxt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.betaTxt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.alphaTxt)).EndInit();
            this.miscEditor.ResumeLayout(false);
            this.miscEditor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.framesTxt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.updateIntervalTxtx)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.musicTrackBar)).EndInit();
            this.devMode.ResumeLayout(false);
            this.devMode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.levelImgBox)).EndInit();
            this.appMenuSettings.ResumeLayout(false);
            this.appMenuSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel editorMainPanel;
        private System.Windows.Forms.TabControl editorTabs;
        private System.Windows.Forms.TabPage objectEditor;
        private System.Windows.Forms.TabPage levelEditor;
        private System.Windows.Forms.TabPage humanEditor;
        private System.Windows.Forms.TabPage threeDEditor;
        private System.Windows.Forms.TabPage missionEditor;
        private System.Windows.Forms.TabPage positionEditor;
        private System.Windows.Forms.TabPage miscEditor;
        private System.Windows.Forms.TextBox modelIDTxt;
        private System.Windows.Forms.Label modelIdLbl;
        private System.Windows.Forms.Button removeModelBtn;
        private System.Windows.Forms.Button removeBuildingsBtn;
        private System.Windows.Forms.Button removeObjsBtn;
        private System.Windows.Forms.ToolTip infoViewer;
        private System.Windows.Forms.Button resetBuildingsBtn;
        private System.Windows.Forms.Button resetObjectsBtn;
        private System.Windows.Forms.Label modelNameOutLbl;
        private System.Windows.Forms.Button removeObjectBtn;
        private System.Windows.Forms.Button removeBuildingBtn;
        private System.Windows.Forms.Button addObjectBtn;
        private System.Windows.Forms.Button addBuildingBtn;
        private System.Windows.Forms.Button resume3DEditorBtn;
        private System.Windows.Forms.Button start3DEditorBtn;
        private System.Windows.Forms.Button exportObjectsBtn;
        private System.Windows.Forms.CheckBox appLogsCb;
        private System.Windows.Forms.CheckBox autoResetCb;
        private System.Windows.Forms.Button resetLevelBtn;
        private System.Windows.Forms.TabPage weaponEditor;
        private System.Windows.Forms.Button installMissionBtn;
        private System.Windows.Forms.TextBox missionDescTxt;
        private System.Windows.Forms.Label missionDescLbl;
        private System.Windows.Forms.Button removeMissionBtn;
        private System.Windows.Forms.TextBox missionNameTxt;
        private System.Windows.Forms.Label missionNameLbl;
        private System.Windows.Forms.Button loadMissionBtn;
        private System.Windows.Forms.Button saveMissionBtn;
        private System.Windows.Forms.ComboBox objectSelectDD;
        private System.Windows.Forms.ComboBox buildingSelectDD;
        private System.Windows.Forms.TabPage aiEditor;
        private System.Windows.Forms.CheckBox xmlCb;
        private System.Windows.Forms.CheckBox jsonCb;
        private System.Windows.Forms.CheckBox csvCb;
        private System.Windows.Forms.Button clearCacheBtn;
        private System.Windows.Forms.Button clearAllLvlBtn;
        private System.Windows.Forms.Button cutsceneRemoveBtn;
        private System.Windows.Forms.CheckBox posCurrentCb;
        private System.Windows.Forms.CheckBox posMeterCb;
        private System.Windows.Forms.CheckBox posOffCb;
        private System.Windows.Forms.TextBox zPosObjTxt;
        private System.Windows.Forms.Label zPosLbl_O;
        private System.Windows.Forms.TextBox yPosObjTxt;
        private System.Windows.Forms.Label yPosLbl_O;
        private System.Windows.Forms.TextBox xPosObjTxt;
        private System.Windows.Forms.Label xPosLbl_O;
        private System.Windows.Forms.Button updateObjPosition;
        private System.Windows.Forms.CheckBox humanPosMeterCb;
        private System.Windows.Forms.CheckBox humanPosOffCb;
        private System.Windows.Forms.TextBox zPosTxt_H;
        private System.Windows.Forms.Label zPosLbl_H;
        private System.Windows.Forms.TextBox yPosTxt_H;
        private System.Windows.Forms.Label yPosLbl_H;
        private System.Windows.Forms.TextBox xPosTxt_H;
        private System.Windows.Forms.Label xPosLbl_H;
        private System.Windows.Forms.Button updateHumaPosition;
        private System.Windows.Forms.ComboBox objectPosDD;
        private System.Windows.Forms.Label objPosLbl;
        private System.Windows.Forms.ComboBox buildingPosDD;
        private System.Windows.Forms.Label buildingPosLbl;
        private System.Windows.Forms.NumericUpDown gammaTxt;
        private System.Windows.Forms.NumericUpDown betaTxt;
        private System.Windows.Forms.NumericUpDown alphaTxt;
        private System.Windows.Forms.Label gammaLbl;
        private System.Windows.Forms.Label betaLbl;
        private System.Windows.Forms.Label alphaLbl;
        private System.Windows.Forms.Button updateObjOrientation;
        private System.Windows.Forms.Button updateHumanHealthBtn;
        private System.Windows.Forms.Label fenceDamageScaleLbl;
        private System.Windows.Forms.Button updateHumanSpeedBtn;
        private System.Windows.Forms.Label upwardJumpLbl;
        private System.Windows.Forms.Label forwardJumpLbl;
        private System.Windows.Forms.Label movementSpeedLbl;
        private System.Windows.Forms.Label inAirSpeedLbl;
        private System.Windows.Forms.Button resetHumanBtn;
        private System.Windows.Forms.Button updatePeekBtn;
        private System.Windows.Forms.Label peekTimeLbl;
        private System.Windows.Forms.Label peekCrouchLbl;
        private System.Windows.Forms.Label peekLRLbl;
        private System.Windows.Forms.Button showAppLogBtn;
        private System.Windows.Forms.CheckBox disableWarningsCb;
        private System.Windows.Forms.Button appSupportBtn;
        private System.Windows.Forms.TabPage graphEditor;
        private System.Windows.Forms.TextBox graphAreaLbl;
        private System.Windows.Forms.ComboBox graphIdDD;
        private System.Windows.Forms.Label graphIdLbl;
        private System.Windows.Forms.ComboBox nodeIdDD;
        private System.Windows.Forms.Label nodeIdLbl;
        private System.Windows.Forms.CheckBox nodesHilightCb;
        private System.Windows.Forms.CheckBox nodesObjectsCb;
        private System.Windows.Forms.Button addNodesBtn;
        private System.Windows.Forms.TextBox nodeCriteriaTxt;
        private System.Windows.Forms.TextBox nodeZTxt;
        private System.Windows.Forms.Label nodeZLbl;
        private System.Windows.Forms.TextBox nodeYTxt;
        private System.Windows.Forms.Label nodeYLbl;
        private System.Windows.Forms.TextBox nodeXTxt;
        private System.Windows.Forms.Label nodeXLbl;
        private System.Windows.Forms.TextBox graphTotalNodesTxt;
        private System.Windows.Forms.Label graphTotalNodesLbl;
        private System.Windows.Forms.CheckBox nodeIdMetreCb;
        private System.Windows.Forms.CheckBox nodeIdOffsetCb;
        private System.Windows.Forms.Button teleportToNodeBtn;
        private System.Windows.Forms.CheckBox viewPortCameraEnableCb;
        private System.Windows.Forms.CheckBox autoTeleportNodeCb;
        private System.Windows.Forms.Button addLevelFlowBtn;
        private System.Windows.Forms.TextBox missionPlayTimeTxt;
        private System.Windows.Forms.Label missionPlayTimeLbl;
        private System.Windows.Forms.CheckBox missionLevelFlowTimerCb;
        private System.Windows.Forms.Button teleportToGraphBtn;
        private System.Windows.Forms.CheckBox autoTeleportGraphCb;
        private System.Windows.Forms.PictureBox objectImgBox;
        private System.Windows.Forms.CheckBox manualTeleportGraphCb;
        private System.Windows.Forms.CheckBox manualTeleportNodeCb;
        private System.Windows.Forms.Button downloadMissionBtn;
        private System.Windows.Forms.Label missionServerList;
        private System.Windows.Forms.Button uploadMissionBtn;
        private System.Windows.Forms.Button stopTraversingNodesBtn;
        private System.Windows.Forms.ComboBox missionsOnlineDD;
        private System.Windows.Forms.Label missionAuthorLbl;
        private System.Windows.Forms.Label missionSizeLbl;
        private System.Windows.Forms.Label missionLevelLbl;
        private System.Windows.Forms.Button missionRefreshBtn;
        private System.Windows.Forms.CheckBox aiIdleCb;
        private System.Windows.Forms.Label damageScaleLbl;
        private System.Windows.Forms.Label teamIdLbl;
        private System.Windows.Forms.Button updateHumanBtn;
        private System.Windows.Forms.Button setFramesBtn;
        private System.Windows.Forms.Label humanViewCamLbl;
        private System.Windows.Forms.CheckBox debugModeCb;
        private System.Windows.Forms.Button udpateMusicBtn;
        private System.Windows.Forms.TrackBar musicTrackBar;
        private System.Windows.Forms.CheckBox enableMusicCb;
        private System.Windows.Forms.CheckBox sfxMusicCb;
        private System.Windows.Forms.CheckBox musicSoundCb;
        private System.Windows.Forms.Button configLoadBtn;
        private System.Windows.Forms.Button configSaveBtn;
        private System.Windows.Forms.Button restoreModelBtn;
        private System.Windows.Forms.Button gfxResetBtn;
        private System.Windows.Forms.CheckBox resetPosCb;
        private System.Windows.Forms.Label gameSettingsLbl;
        private System.Windows.Forms.Label appSettingLbl;
        private System.Windows.Forms.Button addLinksBtn;
        private System.Windows.Forms.Button removeLinksBtn;
        private System.Windows.Forms.Button removeNodesBtn;
        private System.Windows.Forms.Label gameItemsLbl;
        private System.Windows.Forms.CheckBox graphsAllCb;
        private System.Windows.Forms.CheckBox graphsMarkCb;
        private System.Windows.Forms.Button refreshLinksBtn;
        private System.Windows.Forms.Button refreshNodesBtn;
        private System.Windows.Forms.Button editorUpdaterBtn;
        private System.Windows.Forms.CheckBox showAllGraphsCb;
        private System.Windows.Forms.CheckBox updateCheckerCb;
        private System.Windows.Forms.NumericUpDown updateIntervalTxtx;
        private System.Windows.Forms.Label minutesLbl;
        private System.Windows.Forms.TabPage devMode;
        private System.Windows.Forms.Button uploadUpdateBtn;
        private System.Windows.Forms.TextBox devVersionTxt;
        private System.Windows.Forms.Button createUpdateBtn;
        private System.Windows.Forms.Button loadDevFileBtn;
        private System.Windows.Forms.Button saveDevFileBtn;
        private System.Windows.Forms.TextBox devFileSizeTxt;
        private System.Windows.Forms.RichTextBox devViewerTxt;
        private System.Windows.Forms.TextBox devFileNameTxt;
        private System.Windows.Forms.CheckBox devClearContentsCb;
        private System.Windows.Forms.CheckBox devAutoFormatCb;
        private System.Windows.Forms.CheckBox nodesInfoCb;
        private System.Windows.Forms.NumericUpDown framesTxt;
        private System.Windows.Forms.NumericUpDown movementSpeedTxt;
        private System.Windows.Forms.NumericUpDown upwardJumpTxt;
        private System.Windows.Forms.NumericUpDown forwardJumpTxt;
        private System.Windows.Forms.NumericUpDown inAirSpeedTxt;
        private System.Windows.Forms.NumericUpDown peekLRTxt;
        private System.Windows.Forms.NumericUpDown peekCrouchTxt;
        private System.Windows.Forms.NumericUpDown peekTimeTxt;
        private System.Windows.Forms.NumericUpDown damageScaleFenceTxt;
        private System.Windows.Forms.NumericUpDown damageScaleTxt;
        private System.Windows.Forms.NumericUpDown humanViewCamTxt;
        private System.Windows.Forms.NumericUpDown teamIdTxt;
        private System.Windows.Forms.TabControl aiEditorTabs;
        private System.Windows.Forms.TabPage aiEditorMainTab;
        private System.Windows.Forms.TabPage aiJSONEditor;
        private System.Windows.Forms.NumericUpDown maxSpawnsTxt;
        private System.Windows.Forms.NumericUpDown aiCountTxt;
        private System.Windows.Forms.Button removeAiBtn;
        private System.Windows.Forms.TextBox graphAreaAiLbl;
        private System.Windows.Forms.CheckBox taskDetectionAiCb;
        private System.Windows.Forms.CheckBox customAiCb;
        private System.Windows.Forms.Label maxSpawnsLbl;
        private System.Windows.Forms.CheckBox aiFriendlyCb;
        private System.Windows.Forms.CheckBox aiAdvanceViewCb;
        private System.Windows.Forms.CheckBox guardGeneratorCb;
        private System.Windows.Forms.CheckBox aiInvincibleCb;
        private System.Windows.Forms.ComboBox aiGraphIdDD;
        private System.Windows.Forms.ComboBox aiTypeDD;
        private System.Windows.Forms.ComboBox aiWeaponDD;
        private System.Windows.Forms.Label graphIdAiLbl;
        private System.Windows.Forms.Label aiTypeLbl;
        private System.Windows.Forms.Label weaponAiLbl;
        private System.Windows.Forms.PictureBox weaponAIImgBox;
        private System.Windows.Forms.ComboBox aiModelSelectDD;
        private System.Windows.Forms.PictureBox aiImgBox;
        private System.Windows.Forms.Label aiCountLbl;
        private System.Windows.Forms.Label aiModelNameLbl;
        private System.Windows.Forms.Label aiModelLbl;
        private System.Windows.Forms.Button addAiBtn;
        private System.Windows.Forms.TabPage aiScriptEditor;
        private System.Windows.Forms.TabPage aiPatrolPathEditor;
        private System.Windows.Forms.CheckBox aiJsonAutoFmtCb;
        private System.Windows.Forms.CheckBox aiJsonClearDataCb;
        private System.Windows.Forms.RichTextBox aiJsonEditorTxt;
        private System.Windows.Forms.Button aiJsonSaveBtn;
        private System.Windows.Forms.Button aiJsonLoadBtn;
        private System.Windows.Forms.Button aiJsonAddAiBtn;
        private System.Windows.Forms.TextBox aiFileNameTxt;
        private System.Windows.Forms.TextBox aiFileSizeTxt;
        private System.Windows.Forms.TextBox aiJsonVersionTxt;
        private System.Windows.Forms.Button aiJsonSaveAiBtn;
        private System.Windows.Forms.Button gamePathBtn;
        private System.Windows.Forms.Button saveAIBtn;
        private System.Windows.Forms.CheckBox autoRefreshGameCb;
        private System.Windows.Forms.CheckBox aiJsonEditModeCb;
        private System.Windows.Forms.CheckBox externalCompilerCb;
        private System.Windows.Forms.CheckBox internalCompilerCb;
        private System.Windows.Forms.Button resetScriptsFileBtn;
        private System.Windows.Forms.NumericUpDown buildingsResetTxt;
        private System.Windows.Forms.NumericUpDown objectsResetTxt;
        private System.Windows.Forms.NumericUpDown buildingsRemoveTxt;
        private System.Windows.Forms.NumericUpDown objectsRemoveTxt;
        private System.Windows.Forms.NumericUpDown damageScaleFallTxt;
        private System.Windows.Forms.Label fallDamageLbl;
        private System.Windows.Forms.CheckBox removeAllAiCb;
        private System.Windows.Forms.Label modelIdOutLbl;
        private System.Windows.Forms.TextBox modelNameTxt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown teamIdText;
        private System.Windows.Forms.Label teamIdLabel;
        private System.Windows.Forms.CheckBox shareLogsCb;
        private System.Windows.Forms.CheckBox viewLogsCb;
        private System.Windows.Forms.Button showAppDataCachePathBtn;
        private System.Windows.Forms.TabControl weaponEditorTabs;
        private System.Windows.Forms.TabPage weaponEditorMainTab;
        private System.Windows.Forms.TabPage weaponCfgEditor;
        private System.Windows.Forms.TabPage weaponCfgAdvanceEditor;
        private System.Windows.Forms.NumericUpDown weaponAmmoTxt;
        private System.Windows.Forms.CheckBox markWeaponsCb;
        private System.Windows.Forms.CheckBox allWeaponsCb;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox weaponDD;
        private System.Windows.Forms.PictureBox weaponImgBox;
        private System.Windows.Forms.Button removeWeaponBtn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button addWeaponBtn;
        private System.Windows.Forms.ComboBox weaponCfgDD;
        private System.Windows.Forms.Label weaponTypeLblDD;
        private System.Windows.Forms.Button updateWeaponDetailsBtn;
        private System.Windows.Forms.TextBox weaponNameTxt;
        private System.Windows.Forms.Button updateWeaponPropertiesBtn;
        private System.Windows.Forms.Label weaponNameLbl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox weaponDescriptionTxt;
        private System.Windows.Forms.ComboBox weaponDisplayTypeDD;
        private System.Windows.Forms.ComboBox weaponSightTypeDD;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button updateWeaponUIBtn;
        private System.Windows.Forms.TextBox weaponGroupFileTxt;
        private System.Windows.Forms.Button loadWeaponGroupBtn;
        private System.Windows.Forms.Button saveWeaponGroupBtn;
        private System.Windows.Forms.ComboBox weaponSfx2DD;
        private System.Windows.Forms.ComboBox weaponSfx1DD;
        private System.Windows.Forms.Label sfx2Lbl;
        private System.Windows.Forms.Label sfx1Lbl;
        private System.Windows.Forms.Button updateWeaponSFXBtn;
        private System.Windows.Forms.NumericUpDown weaponRoundPerMinuteTxt;
        private System.Windows.Forms.NumericUpDown weaponBulletsTxt;
        private System.Windows.Forms.NumericUpDown weaponPowerTxt;
        private System.Windows.Forms.NumericUpDown weaponDamageTxt;
        private System.Windows.Forms.Label weaponRoundPerMinuteLbl;
        private System.Windows.Forms.Button updateWeaponPowerDamageBtn;
        private System.Windows.Forms.Label weaponBulletsLbl;
        private System.Windows.Forms.Label weaponPowerLbl;
        private System.Windows.Forms.Label weaponDamageLbl;
        private System.Windows.Forms.NumericUpDown weaponRoundPerClipTxt;
        private System.Windows.Forms.Label weaponRoundPerClipLbl;
        private System.Windows.Forms.NumericUpDown weaponRangeTxt;
        private System.Windows.Forms.Label weaponRangeLbl;
        private System.Windows.Forms.Button resetWeaponBtn;
        private System.Windows.Forms.CheckBox currentWeaponCb;
        private System.Windows.Forms.Button quitLevelBtn;
        private System.Windows.Forms.NumericUpDown levelStartTxt;
        private System.Windows.Forms.Button restartLevelBtn;
        private System.Windows.Forms.Button startGameBtn;
        private System.Windows.Forms.Label xPosLbl;
        private System.Windows.Forms.Label yPosLbl;
        private System.Windows.Forms.Label zPosLbl;
        private System.Windows.Forms.Label statusTxt;
        private System.Windows.Forms.Label zPosLbl_M;
        private System.Windows.Forms.Label yPosLbl_M;
        private System.Windows.Forms.Label xPosLbl_M;
        private System.Windows.Forms.Label levelNameLbl;
        private System.Windows.Forms.PictureBox levelImgBox;
        private System.Windows.Forms.MenuStrip appMenuSettings;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editorOnlineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectEditorModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playModeCb;
        private System.Windows.Forms.ToolStripMenuItem editorModeCb;
        private System.Windows.Forms.ToolStripMenuItem appSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gameSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem applicationSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editorConnectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editorOnlineCb;
        private System.Windows.Forms.ToolStripMenuItem startGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startWindowedGameBtn;
        private System.Windows.Forms.ToolStripMenuItem startFullScreenGameBtn;
        private System.Windows.Forms.ToolStripMenuItem startGameBtnMenu;
        private System.Windows.Forms.ToolStripTextBox levelStartTxtMenu;
        private System.Windows.Forms.ToolStripMenuItem restartLevelBtnMenu;
        private System.Windows.Forms.ToolStripMenuItem quitLevelBtnMenu;
        private System.Windows.Forms.ToolStripMenuItem versionToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox versionLbl;
        private System.Windows.Forms.ToolStripMenuItem gameStatusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compilerToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox compilerTypeLbl;
        private System.Windows.Forms.ToolStripMenuItem profileToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox gameProfileNameLbl;
        private System.Windows.Forms.ToolStripMenuItem liveEditorCb;
        private System.Windows.Forms.ToolStripMenuItem positionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem posCoordCb;
        private System.Windows.Forms.ToolStripMenuItem posMetersCb;
        private System.Windows.Forms.ToolStripMenuItem refreshGameBtnMenu;
        private System.Windows.Forms.ToolStripMenuItem internalsStatusMenu;
        private System.Windows.Forms.ToolStripMenuItem internalsStatusLbl;
        private System.Windows.Forms.ToolStripMenuItem gameProfileMissionLbl;
    }
}

