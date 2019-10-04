import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { HeroDetailComponent } from './hero-detail/hero-detail.component';
import { HeroesComponent } from './heroes/heroes.component';
import { HeroSearchComponent } from './hero-search/hero-search.component';
import { MessagesComponent } from './messages/messages.component';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { NavComponent } from './nav/nav.component';
import { FooterComponent } from './footer/footer.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { AddHeroComponent } from './add-hero/add-hero.component';
import { HeroesMainContentComponent } from './heroes-main-content/heroes-main-content.component';
import { AddHeroMainComponent } from './add-hero-main/add-hero-main.component';
import { JwPaginationComponent } from 'jw-angular-pagination';
import { ReactiveFormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';
import { LayoutComponent } from './layout/layout.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { Globals } from './globals';

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
    LayoutComponent
  ],
  providers: [
    // ... other global providers
    Globals // so do not provide it into another components/services if you want it to be a singleton
  ],
  bootstrap: [AppComponent]
})

export class AppModule { }
