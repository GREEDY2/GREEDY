import Cookies from 'universal-cookie';
import Constants from './Constants';

export function GetCredentialsFromCookies() {
    const cookies = new Cookies();
    let Username = cookies.get(Constants.cookieUsername);
    let SessionId = cookies.get(Constants.cookieSessionId);
    let credentials = {
        Username,
        SessionId
    }
    return credentials;
}

export function RemoveCredentialsFromCookies() {
    const cookies = new Cookies();
    cookies.remove(Constants.cookieUsername, { path: '/' });
    cookies.remove(Constants.cookieSessionId, { path: '/' });
}

