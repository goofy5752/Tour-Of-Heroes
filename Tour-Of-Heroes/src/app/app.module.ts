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

@NgModule({
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    NgbModule,
    ReactiveFormsModule
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
    JwPaginationComponent
  ],
  bootstrap: [AppComponent]
})

export class AppModule { }
