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
import { AddHeroComponent } from './admin/add-hero/add-hero.component';
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
import { AdminComponent } from './admin/admin.component';
import { BlogDetailComponent } from './content/blog-detail/blog-detail.component';
import { AddBlogComponent } from './admin/add-blog/add-blog.component';
import { ForbiddenComponent } from './user/forbidden/forbidden.component';
import { AngularEditorModule } from '@kolkov/angular-editor';
import { OrderModule } from 'ngx-order-pipe';
import { FilterPipeModule } from 'ngx-filter-pipe';
import { UserControllerComponent } from './admin/user-controller/user-controller.component';
import { UserDetailComponent } from './admin/user-detail/user-detail.component';
import { LikedMoviesComponent } from './user/liked-movies/liked-movies.component';
import { MDBBootstrapModule } from 'angular-bootstrap-md';
import { BlogEditComponent } from './content/blog-edit/blog-edit.component';

@NgModule({
  imports: [
    BrowserModule,
    OrderModule,
    FilterPipeModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    NgbModule,
    ReactiveFormsModule,
    AngularEditorModule,
    NgxPaginationModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    MDBBootstrapModule.forRoot(),
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
    AddBlogComponent,
    ForbiddenComponent,
    UserControllerComponent,
    UserDetailComponent,
    LikedMoviesComponent,
    BlogEditComponent,
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
