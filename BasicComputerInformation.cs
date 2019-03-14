using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace orcinnect
{
	// Token: 0x02000295 RID: 661
	[Serializable]
	public class PluginInfo
	{
		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x0600133C RID: 4924 RVA: 0x0000F1DF File Offset: 0x0000D3DF
		// (set) Token: 0x0600133D RID: 4925 RVA: 0x0000F1E7 File Offset: 0x0000D3E7
		public Guid Guid { get; set; }

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x0600133E RID: 4926 RVA: 0x0000F1F0 File Offset: 0x0000D3F0
		// (set) Token: 0x0600133F RID: 4927 RVA: 0x0000F1F8 File Offset: 0x0000D3F8
		public string Name { get; set; }

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06001340 RID: 4928 RVA: 0x0000F201 File Offset: 0x0000D401
		// (set) Token: 0x06001341 RID: 4929 RVA: 0x0000F209 File Offset: 0x0000D409
		public string Version { get; set; }

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06001342 RID: 4930 RVA: 0x0000F212 File Offset: 0x0000D412
		// (set) Token: 0x06001343 RID: 4931 RVA: 0x0000F21A File Offset: 0x0000D41A
		public bool IsLoaded { get; set; }
	}
	
	// Token: 0x0200028E RID: 654
	[Serializable]
	public class LoadablePlugin
	{
		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x060012FE RID: 4862 RVA: 0x0000EFF2 File Offset: 0x0000D1F2
		// (set) Token: 0x060012FF RID: 4863 RVA: 0x0000EFFA File Offset: 0x0000D1FA
		public Guid Guid { get; set; }

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06001300 RID: 4864 RVA: 0x0000F003 File Offset: 0x0000D203
		// (set) Token: 0x06001301 RID: 4865 RVA: 0x0000F00B File Offset: 0x0000D20B
		public string Version { get; set; }
	}
	
	public enum OSType : byte
	{
		// Token: 0x040008CB RID: 2251
		[Description("Unknown")]
		Unknown,
		// Token: 0x040008CC RID: 2252
		[Description("Windows XP")]
		WindowsXp,
		// Token: 0x040008CD RID: 2253
		[Description("Windows Vista")]
		WindowsVista,
		// Token: 0x040008CE RID: 2254
		[Description("Windows 7")]
		Windows7,
		// Token: 0x040008CF RID: 2255
		[Description("Windows 8")]
		Windows8,
		// Token: 0x040008D0 RID: 2256
		[Description("Windows 10")]
		Windows10
	}
	public enum PluginSettingType
	{
		// Token: 0x04000E86 RID: 3718
		ClientPlugin,
		// Token: 0x04000E87 RID: 3719
		BuildPlugin
	}
	
	// Token: 0x0200036A RID: 874
	[Serializable]
	public class PluginSetting : ClientSetting
	{
		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06001801 RID: 6145 RVA: 0x00011890 File Offset: 0x0000FA90
		// (set) Token: 0x06001802 RID: 6146 RVA: 0x00011898 File Offset: 0x0000FA98
		[XmlAttribute]
		public Guid PluginId { get; set; }

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06001803 RID: 6147 RVA: 0x000118A1 File Offset: 0x0000FAA1
		// (set) Token: 0x06001804 RID: 6148 RVA: 0x000118A9 File Offset: 0x0000FAA9
		[XmlAttribute]
		public PluginSettingType PluginType { get; set; }
	}
	
	[XmlInclude(typeof(PluginSetting))]
	[Serializable]
	public class ClientSetting
	{
		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x060017FC RID: 6140 RVA: 0x0001186E File Offset: 0x0000FA6E
		// (set) Token: 0x060017FD RID: 6141 RVA: 0x00011876 File Offset: 0x0000FA76
		[XmlAttribute]
		public string SettingsType { get; set; }

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x060017FE RID: 6142 RVA: 0x0001187F File Offset: 0x0000FA7F
		// (set) Token: 0x060017FF RID: 6143 RVA: 0x00011887 File Offset: 0x0000FA87
		public List<PropertyNameValue> Properties { get; set; }
	}
	[Serializable]
	public class PropertyNameValue
	{
		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x060012AD RID: 4781 RVA: 0x0000ED7D File Offset: 0x0000CF7D
		// (set) Token: 0x060012AE RID: 4782 RVA: 0x0000ED85 File Offset: 0x0000CF85
		public string Name { get; set; }

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x060012AF RID: 4783 RVA: 0x0000ED8E File Offset: 0x0000CF8E
		// (set) Token: 0x060012B0 RID: 4784 RVA: 0x0000ED96 File Offset: 0x0000CF96
		public object Value { get; set; }
	}
	
	// Token: 0x0200036C RID: 876
	[Serializable]
	public enum ResourceType : byte
	{
		// Token: 0x04000E90 RID: 3728
		Command,
		// Token: 0x04000E91 RID: 3729
		ClientPlugin,
		// Token: 0x04000E92 RID: 3730
		FactoryCommand
	}
	
	// Token: 0x0200036B RID: 875
	[Serializable]
	public class PluginResourceInfo
	{
		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x0600180B RID: 6155 RVA: 0x000118DC File Offset: 0x0000FADC
		// (set) Token: 0x0600180C RID: 6156 RVA: 0x000118E4 File Offset: 0x0000FAE4
		public string ResourceName { get; set; }

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x0600180D RID: 6157 RVA: 0x000118ED File Offset: 0x0000FAED
		// (set) Token: 0x0600180E RID: 6158 RVA: 0x000118F5 File Offset: 0x0000FAF5
		public ResourceType ResourceType { get; set; }

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x0600180F RID: 6159 RVA: 0x000118FE File Offset: 0x0000FAFE
		// (set) Token: 0x06001810 RID: 6160 RVA: 0x00011906 File Offset: 0x0000FB06
		public Guid Guid { get; set; }

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06001811 RID: 6161 RVA: 0x0001190F File Offset: 0x0000FB0F
		// (set) Token: 0x06001812 RID: 6162 RVA: 0x00011917 File Offset: 0x0000FB17
		public string PluginName { get; set; }

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06001813 RID: 6163 RVA: 0x00011920 File Offset: 0x0000FB20
		// (set) Token: 0x06001814 RID: 6164 RVA: 0x00011928 File Offset: 0x0000FB28
		public string PluginVersion { get; set; }
	}
	
	[Serializable]
	public class ClientConfig
	{
		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06001806 RID: 6150 RVA: 0x000118BA File Offset: 0x0000FABA
		// (set) Token: 0x06001807 RID: 6151 RVA: 0x000118C2 File Offset: 0x0000FAC2
		public List<PluginResourceInfo> PluginResources { get; set; }

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001808 RID: 6152 RVA: 0x000118CB File Offset: 0x0000FACB
		// (set) Token: 0x06001809 RID: 6153 RVA: 0x000118D3 File Offset: 0x0000FAD3
		public List<ClientSetting> Settings { get; set; }
	}
	// Token: 0x0200029E RID: 670
	[Serializable]
	public class BasicComputerInformation
	{
		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06001372 RID: 4978 RVA: 0x0000F38B File Offset: 0x0000D58B
		// (set) Token: 0x06001373 RID: 4979 RVA: 0x0000F393 File Offset: 0x0000D593
		public string UserName { get; set; }

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06001374 RID: 4980 RVA: 0x0000F39C File Offset: 0x0000D59C
		// (set) Token: 0x06001375 RID: 4981 RVA: 0x0000F3A4 File Offset: 0x0000D5A4
		public string OperatingSystemName { get; set; }

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06001376 RID: 4982 RVA: 0x0000F3AD File Offset: 0x0000D5AD
		// (set) Token: 0x06001377 RID: 4983 RVA: 0x0000F3B5 File Offset: 0x0000D5B5
		public OSType OperatingSystemType { get; set; }

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06001378 RID: 4984 RVA: 0x0000F3BE File Offset: 0x0000D5BE
		// (set) Token: 0x06001379 RID: 4985 RVA: 0x0000F3C6 File Offset: 0x0000D5C6
		public string Language { get; set; }

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x0600137A RID: 4986 RVA: 0x0000F3CF File Offset: 0x0000D5CF
		// (set) Token: 0x0600137B RID: 4987 RVA: 0x0000F3D7 File Offset: 0x0000D5D7
		public bool IsAdministrator { get; set; }

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x0600137C RID: 4988 RVA: 0x0000F3E0 File Offset: 0x0000D5E0
		// (set) Token: 0x0600137D RID: 4989 RVA: 0x0000F3E8 File Offset: 0x0000D5E8
		public bool IsServiceRunning { get; set; }

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x0600137E RID: 4990 RVA: 0x0000F3F1 File Offset: 0x0000D5F1
		// (set) Token: 0x0600137F RID: 4991 RVA: 0x0000F3F9 File Offset: 0x0000D5F9
		public List<PluginInfo> Plugins { get; set; }

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06001380 RID: 4992 RVA: 0x0000F402 File Offset: 0x0000D602
		// (set) Token: 0x06001381 RID: 4993 RVA: 0x0000F40A File Offset: 0x0000D60A
		public List<LoadablePlugin> LoadablePlugins { get; set; }

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06001382 RID: 4994 RVA: 0x0000F413 File Offset: 0x0000D613
		// (set) Token: 0x06001383 RID: 4995 RVA: 0x0000F41B File Offset: 0x0000D61B
		public List<int> ActiveCommands { get; set; }

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06001384 RID: 4996 RVA: 0x0000F424 File Offset: 0x0000D624
		// (set) Token: 0x06001385 RID: 4997 RVA: 0x0000F42C File Offset: 0x0000D62C
		public ClientConfig ClientConfig { get; set; }

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06001386 RID: 4998 RVA: 0x0000F435 File Offset: 0x0000D635
		// (set) Token: 0x06001387 RID: 4999 RVA: 0x0000F43D File Offset: 0x0000D63D
		public int ClientVersion { get; set; }

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06001388 RID: 5000 RVA: 0x0000F446 File Offset: 0x0000D646
		// (set) Token: 0x06001389 RID: 5001 RVA: 0x0000F44E File Offset: 0x0000D64E
		public string ClientPath { get; set; }

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x0600138A RID: 5002 RVA: 0x0000F457 File Offset: 0x0000D657
		// (set) Token: 0x0600138B RID: 5003 RVA: 0x0000F45F File Offset: 0x0000D65F
		public int ApiVersion { get; set; }

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x0600138C RID: 5004 RVA: 0x0000F468 File Offset: 0x0000D668
		// (set) Token: 0x0600138D RID: 5005 RVA: 0x0000F470 File Offset: 0x0000D670
		public double FrameworkVersion { get; set; }

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x0600138E RID: 5006 RVA: 0x0000F479 File Offset: 0x0000D679
		// (set) Token: 0x0600138F RID: 5007 RVA: 0x0000F481 File Offset: 0x0000D681
		public byte[] MacAddress { get; set; }
	}
}
