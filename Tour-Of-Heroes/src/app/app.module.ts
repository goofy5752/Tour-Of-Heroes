import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserModule, Title } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { HeroDetailComponent } from './content/hero-detail/hero-detail.component';
import { HeroesComponent } from './content/heroes/heroes.component';
import { HeroSearchComponent } from './layout-view/hero-search/hero-search.component';
import { MessagesComponent } from './content/messages/messages.component';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { NavComponent } from './layout-view/nav/nav.component';
import { FooterComponent } from './layout-view/footer/footer.component';
import { SidebarComponent } from './layout-view/sidebar/sidebar.component';
import { AddHeroComponent } from './content/add-hero/add-hero.component';
import { HeroesMainContentComponent } from './main-content/heroes-main-content/heroes-main-content.component';
import { AddHeroMainComponent } from './main-content/add-hero-main/add-hero-main.component';
import { JwPaginationComponent } from 'jw-angular-pagination';
import { ReactiveFormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';
import { LayoutComponent } from './layout-view/layout/layout.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { Globals } from './globals/globals';
import { UserComponent } from './user/user.component';
import { LoginComponent } from './user/login/login.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { UserService } from './services/user.service';
import { AuthInterceptor } from './auth/auth.interceptor';
import { ConfirmationPopoverModule } from 'angular-confirmation-popover';
import { ProfileComponent } from './user/profile/profile.component';
import { BlogComponent } from './content/blog/blog.component';
import { AdminComponent } from './user/admin/admin.component';
import { BlogDetailComponent } from './content/blog-detail/blog-detail.component';

@NgModule({
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    NgbModule,
    ReactiveFormsModule,
    NgxPaginationModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    ConfirmationPopoverModule.forRoot({
      confirmButtonType: 'danger' // set defaults here
    })
  ],
  declarations: [
    AppComponent,
    HeroesComponent,
    HeroDetailComponent,
    MessagesComponent,
    HeroSearchComponent,
    NavComponent,
    FooterComponent,
    SidebarComponent,
    AddHeroComponent,
    HeroesMainContentComponent,
    AddHeroMainComponent,
    JwPaginationComponent,
    LayoutComponent,
    UserComponent,
    LoginComponent,
    RegistrationComponent,
    ProfileComponent,
    BlogComponent,
    AdminComponent,
    BlogDetailComponent,
  ],
  providers: [
    [UserService, {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }],
    Globals, // so do not provide it into another components/services if you want it to be a singleton
    Title
  ],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})

export class AppModule { }
