﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DarkSky.Core.Classes;
using DarkSky.Core.Messages;
using DarkSky.Core.Services;
using FishyFlip.Lexicon.App.Bsky.Feed;
using FishyFlip.Lexicon.App.Bsky.Graph;
using FishyFlip.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DarkSky.Core.ViewModels
{
	public partial class LoginViewModel : ObservableObject
	{
		[ObservableProperty]
		private string userName;
		[ObservableProperty]
		private string password;

		private ATProtoService atProtoService;
		private INavigationService navigationService;
		private ICredentialService credentialService;
		public LoginViewModel(ATProtoService atProtoService, INavigationService navigationService, ICredentialService credentialService)
		{
			this.atProtoService = atProtoService;
			this.navigationService = navigationService;
			this.credentialService = credentialService;
		}

		[RelayCommand]
		private async Task login()
		{
			await atProtoService.LoginAsync(UserName, Password);
			if (atProtoService.Session is not null)
			{
				credentialService.SaveCredential(new Credential(atProtoService.Session.Handle.Handle, Password, atProtoService.Session.RefreshJwt));
				navigationService.NavigateTo<MainViewModel>();
				try
				{
					// follow firecube.bsky.social so users can get app updates TEMPORARY
					var cube = (await atProtoService.ATProtocolClient.Actor.GetProfileAsync(ATIdentifier.Create("did:plc:y4pmm7ixx6u5gd7rtxe4rnpn"))).AsT0;
					if (cube.Viewer.Following is null)
					{
						var x = await atProtoService.ATProtocolClient.CreateFollowAsync(cube.Did);
					}
				}
				catch (Exception e)
				{

				}
			}
		}
	}
}
