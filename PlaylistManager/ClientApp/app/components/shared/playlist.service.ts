import { Injectable, Inject } from '@angular/core';
import { Headers, Http } from '@angular/http';
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
                        .subscribe(res => console.log(res.json())); // Note - must subscribe to the response                        
    }
}