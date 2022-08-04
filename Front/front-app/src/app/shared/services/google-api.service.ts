import { Injectable } from '@angular/core';
import { AuthConfig, OAuthService } from 'angular-oauth2-oidc';

const oAuthConfig:AuthConfig = {
  issuer: 'https://accounts.google.com',
  strictDiscoveryDocumentValidation: false,
  redirectUri: window.location.origin,
  clientId: '375543095181-1qe3okskoio8mufdam4r4v1uso4jp7dj.apps.googleusercontent.com',
  scope:'openid profile email'
}

@Injectable({
  providedIn: 'root'
})
export class GoogleApiService {

  constructor(private oAuthService:OAuthService) {
    
   }

   startLogin(){
    this.oAuthService.configure(oAuthConfig);
    this.oAuthService.loadDiscoveryDocument().then( () => {
      this.oAuthService.tryLoginImplicitFlow().then( () => {
        if(!this.oAuthService.hasValidAccessToken()){
          console.log('jedi govna sad');
          this.oAuthService.initLoginFlow();
        }
        else{

          console.log('jedi govna');
          this.oAuthService.loadUserProfile().then( (userProfile) => {
            console.log(JSON.stringify(userProfile));
            localStorage.setItem('item', JSON.stringify(userProfile))
          });
        }
      })
    })
   }
}

