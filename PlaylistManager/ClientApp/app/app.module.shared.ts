import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { CounterComponent } from './components/counter/counter.component';
import { AllMusicComponent } from './components/all-music/all-music.component';
import { PagerComponent } from './components/pager/pager.component';
import { AddPlaylistComponent } from './components/add-playlist/add-playlist.component';


@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        HomeComponent,
        AllMusicComponent,
        PagerComponent,
        AddPlaylistComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'all-music', component: AllMusicComponent },
            { path: 'add-playlist', component: AddPlaylistComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ]
})

export class AppModuleShared {
}
