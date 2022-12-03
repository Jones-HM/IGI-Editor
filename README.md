# Project I.G.I Editor

[Project I.G.I](https://en.wikipedia.org/wiki/Project_I.G.I.) Editor is all in one game editor which lets you edit all game objects including _Buildings/3D/AI/Weapons_ and lets you to design your own _custom level_ in the game and lets you _upload/download_ your level to server and share with your friends.

## Pre-Requisite
- **General section.**
- [DLL File](https://en.wikipedia.org/wiki/Dynamic-link_library) - This project uses [IGI Natives.dll](https://github.com/IGI-Research-Devs/IGI_Internal/) file and inject that into Game to call game native methods. .</br>
- [DLL Injection](https://en.wikipedia.org/wiki/DLL_injection) - This project needs DLL injection into _IGI_ game.</br>
- **Game specific section.**
- [IGI Graphs Structure](https://github.com/IGI-Research-Devs/IGI-Research-Data/blob/main/Research/GRAPH/Graph-Structure.txt) - Project IGI 1 Graph structure data.
- [IGI 3D Models](https://github.com/IGI-Research-Devs/IGI-Research-Data/blob/main/Research/Natives/IGI-Models.txt) - Project IGI uses 3D models in 
form of _MEF_ (**M**esh **E**xternal **F**ile).
- [IGI Camera View](https://www.researchgate.net/figure/Definition-of-pitch-roll-and-yaw-angle-for-camera-state-estimation_fig15_273225757) - IGI use game Camera called [Viewport](https://en.wikipedia.org/wiki/Viewport) to display the game.

## Editor components.
Editor has several components which it needs in order to work fully to its functionality.
- Server : Located at [OrgFree](http://igiresearchdevelopers.orgfree.com/) which contains Mission files and Resources like Weapon/A.I images for editor.
- Database : Located at Github Gist private repo contains privacy information like _I.P,Mac Address_ and _Key_ for Editor.
- Internal DLL : Located at [IGI-Internals](https://github.com/IGI-Research-Devs/IGI_Internal) is used to call IGI Game internal native methods.
- QEditor - Located at path `C:\Users\my_username\AppData\Roaming\QEditor`  This is appdata file comes pre-installed with any version of editor and can be downloaded from here [QEditor Full Version](https://cutt.ly/p1ASiQX).</br>

## QEditor components.
**Q**Editor was the initial name of the project because editor actually modifies **Q** files of games like _QVM,QSC,QAS_ files but later it was changed.</br>

    .
    ├── AIFiles                 # AI Script and Path files.
    ├── QCompiler               # Compiler/Decompiler for IGI.
    ├── QFiles                  # Source files - QSC, Binary files - QVM.
    ├── QGraphs                 # Graphs files and areas.
    ├── QMissions               # Custom Missions files.
    ├── QWeapons                # Custom Weapons files.
    ├── Void                    # Empty Mission files.
    ├── aiIdle.qvm              # File for setting AI to idle state.
    ├── IGIModels.txt           # Contains 3D Models information.
    ├── keywords.txt            # Keywords for QVM Editor.
    ├── QChecks.dat             # Contain MD5 Hashes to check file integrity.
    └── weaponconfig.qvm        # Weapon config file.


## Editor Working flow.
Editor most of the time just compiles the script file _QSC_ called _Q_ script source into _QVM_ called _Q_ virtual machine and Restart the level in order to see the affected changes. 
Here is workflow mentioned.
1. Get game level and select proper source file of level located at `QEditor\QFiles\IGI_QSC\missions\location0\level`.</br>
2. Updates the script file with new script depends upon logic Add/Update/Delete commands.</br>
**Internal compiler**</br>
3. Copies script file from Editor `C:\IGI-Editor` to `D:\IGI\` and compiles them to binary _QVM_ file and moves them to proper destination.</br>
**External compiler**</br>
3. Copies script file from Editor `C:\IGI-Editor` to `QEditor\QCompiler\Compile\input` and compiles them to binary _QVM_ file and convert compiled QVM v7 (IGI 2) to QVM v5 (IGI 1) using _DConv_ tool and moves them to proper destination.</br>
4. Restart the level and see the changes.</br>

## Editor sections.
### Level Editor:
**Level Editor**: Lets you to `Add` or `Remove` Building/Objects in level at any position you want select list of objects to add in level and add then in `Edit mode`.</br>

![](https://i.ibb.co/qJrt9fx/IGI-Editor-Main.png)

### Object Editor
**Object Editor**: Lets you to `Remove` or `Restore` objects in level `permanently` or `temporary` depends which mode is selected `Live` or `Normal` mode.</br>

![](https://i.ibb.co/yqjspnJ/IGI-Editor-Object-Ed.png)

### Human Editor:
**Human Editor**: Lets you to Update human Speed/Jump,Health Scale peek and Team Id/Human Camera (1st Person,3rd person).</br>

![](https://i.ibb.co/hZxhghk/IGI-Editor-Human-Ed.png)

### Weapon Editor:
**Weapon Editor**: Lets you to `Add` or `Remove` new weapons in level `permanently` or `temporary` depends which mode is selected `Live` or `Normal` mode.</br>

![](https://i.ibb.co/269CSkz/IGI-Editor-Weapon-Ed.png)

### A.I Editor:
**A.I Editor**: Lets you to `Add` or `Remove` `Friendly/Enemy` A.I into level with various properties like _invincible_,_advance view_,_guard generator_ etc.</br>

![](https://i.ibb.co/RPNf61h/IGI-Editor-AI-Main-Ed.png)

### A.I JSON Editor.
**JSON Editor**: Lets you to `Save` or `Load` A.I to `JSON` file _permanently_ for later use, and you can edit/share json files into editor.</br>
![](https://i.ibb.co/GMY6fmz/IGI-Editor-AI-JSONEd.png)


### Mission Editor
**Mission Editor**: Lets you to `Add` or `Remove` `Missions` into level after you design your own custom level enter _Name_ and _Description_ and **Save** your Mission</br>
You can also **Upload** or **Download** new missions from _Server_ and load into your game and play them directly..</br>
This section supports **[ONLINE MODE]**.
![](https://i.ibb.co/xG0QMnC/IGI-Editor-Mission-Ed.png)

### Graph Editor.</br> 
**Graph editor**: Lets you see visualization of Graph and nodes information, You can `teleport` or `Auto traverse` to Graph or Nodes in _real time_ and see where graph or nodes are in selected level..</br>

![](https://i.ibb.co/17B96Z5/IGI-Editor-Graph-Ed.png)

### Graph & Links - Level 5. ( IGI 1 Editor).
![](https://i.ibb.co/px7fWfS/Node-Links-L5-Area2.png)

### Graph & Links - Level 1. ( IGI 2 Editor).
![](https://i.ibb.co/gR1vZSM/IGI2-Graph-Nodes.png)

### Misc Editor:
**Misc editor**: Lets you to `Reset/Remove` levels back to normal and `Remove Cutscenes`, Export level data,Set Game Frames `FPS` Update Game Music and more.</br>
![](https://i.ibb.co/GT0kbtR/IGI-Editor-Misc-Ed.png)

### IGI Editor License Key:
~~This editor requires _license key_ to run contact via _email_ or _discord_ and mention your **Username** to identify yourself.</br>
And now you can generate Key from KeyGen for your editor for free.</br>~~

### IGI Editor demo limit:
This editor only offers _3 Levels_, and some features are disabled at the moment like **3D/Resource/Compiler/PlayerProfile editor.**</br> 

## Full version includes:
* All **14 levels** with More **advanced editor**.
* **Real Time Editor Support**.
* **Resource/Profile/GameConfig/QCompiler** editor.
* **Compiler** and **Assembler** with _Parser_ output like **.qsc** to **.qas** to **.qvm**.

### Profile Editor: **_Available in Full version_**.</br> 
**Profile editor**: Lets you manage your `Game Profiles` you can update _Name,Level,Mission_ details or  `Add` or `Remove` new player profile for your player.</br>

### Resource Editor: **_Available in Full version_**.</br> 
**Resource editor**: Lets you see all details of Game resources information like Resource _Name,Address,Id_ and can `Load` or `Unload` any resource in _Real time_.</br>

### GameConfig Editor: **_Available in Full version_**.</br> 
**GameConfig editor**: Lets you edit game configuration like _Level,Password,ActiveMission,Graphics/Music/Mouse settings,Game Key controls,Game difficulty_ and more.</br> 

### QCompiler Editor: **_Available in Full version_**.</br> 
**QCompiler editor**: Lets you `Compile/Assemble/Parse` game files which game uses internaly to save/edit game data.</br>

# Editor Tutorial on YouTube :
[![QEditor](https://img.youtube.com/vi/zH0a8Ma_tQ8/0.jpg)](https://www.youtube.com/watch?v=zH0a8Ma_tQ8)

## Version Update:
**Editor version 0.7.0.0 Latest.**

## Full version of Editor.
If you're a developer and know how to build this tool by yourself then you could figure our the _MAX_LEVEL_ constant defined in editor to unlock the full version. Good Luck.

# **DOWNLOAD LINKS**</br>
- **Project I.G.I 1 Editor** Version 0.7.0.0 _RELEASED_</br>
[IGI1Editor_0.7_Setup](https://cutt.ly/11GUGGO)</br>

If you encounter any issues with Editor just contact me at</br>

- Discord Id: _Jones_IGI#3954_ and Join [Discord server](https://discord.gg/AyVDW7kE6V)</br>
- Email: igiproz.hm@gmail.com</br>
- Follow Project: [GitHub](https://github.com/IGI-Research-Devs/)</br>
- Subscribe Channel: [YouTube](https://www.youtube.com/channel/UChGryl0a0dii81NfDZ12LwA/)</br>

Original Author : _HeavenHM@2022_.
