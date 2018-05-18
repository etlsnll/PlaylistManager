﻿import { Injectable, Inject } from '@angular/core';
import { Headers, Http, URLSearchParams } from '@angular/http';
import { Observable } from "rxjs";
import { Playlist } from '../../playlist'; // Import the form model

const httpOptions = {
    headers: new Headers({
        'Content-Type': 'application/json'
    })
};

@Injectable()
export class PlaylistService {

    private url: string;
    private http: Http;
    private totalPlaylists: number = 0;

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.url = baseUrl;
        this.http = http;
    }

    handleErrorObservable(error: Response | any) {
        console.error(error.message || error);
        return Observable.throw(error.message || error);
    } 

    addPlaylist(playlist: Playlist) {        
        return this.http.post(this.url + 'api/MusicLibrary/AddPlayList', playlist)
                        .catch(this.handleErrorObservable)
                        .map(response => response.json() as number);
                        //.subscribe(res => console.log(res)); // Note - must subscribe to the response even if not interested for POST to work                       
    }

    countPlaylists() {
        return this.http.get(this.url + 'api/MusicLibrary/CountAllPlaylists')
                        .catch(this.handleErrorObservable)
                        .map(response => response.json() as number);        
    }

    getPlaylists(pageNum: number, pageSize: number) {
        var search = new URLSearchParams();
        search.set('pageNum', pageNum.toString()); // Add URL query param
        search.set('pageSize', pageSize.toString()); // Add URL query param
        return this.http.get(this.url + 'api/MusicLibrary/AllPlaylists', { search: search })
                        .catch(this.handleErrorObservable)
                        .map(response => response.json() as PlaylistSummary[]);
    }

    getPlaylist(id: number) {
        //console.log("getPlaylist() - Playlist id: " + id);
        return this.http.get(this.url + 'api/MusicLibrary/Playlist/' + id.toString())
                        .catch(this.handleErrorObservable)
                        .map(response => response.json() as PlaylistDetails);
    }
}

export class Track {

    constructor() { }

    public trackId: number;
    public album: string;
    public albumArtist: string | null;
    public artist: string;
    public year: number | null;
    public title: string;
    public trackNum: number | null;
}

export interface PlaylistSummary {
    id: number;
    name: string;
    numTracks: number;
}

export class PlaylistDetails {
    constructor(
        public id: number,
        public name: string,
        public numTracks: number,
        public tracks: Track[]
    ) { }
}