import { Injectable, Inject } from '@angular/core';
import { Headers, Http } from '@angular/http';
import { Observable } from "rxjs";
import { Playlist } from '../../playlist'; // Import the form model

const httpOptions = {
    headers: new Headers({
        'Content-Type': 'application/json',
        'Authorization': 'my-auth-token'
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

    addPlaylist(playlist: Playlist): Observable<Playlist> {
        return this.http.post(this.url + 'api/MusicLibrary/AddPlayList', playlist, httpOptions)
            .mapTo(this.extractData)
            .catch(this.handleErrorObservable);
    }

    private extractData(res: Response) {
        let body = res.json();
        console.log("Response back from server: " + res);
        return body || {};
    }
}