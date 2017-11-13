import * as React from 'react';

export const Constants = {
    minPasswordLength: 5,
    minUsernameLength: 5,
    maxAnyInputLength: 256,
    emailRegex: /^(([^<>()\[\]\.,;:\s@\"]+(\.[^<>()\[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/,
    httpRequestBasePath: 'http://localhost:6967/',
    cookieUsername: 'username',
    cookieSessionId: 'sessionID',
    checkIfUserlogedInTimer: 1000,
    displayAlertMessage: 5000,
    offsetAlertMessage: 15,
    themeAlertMessage: 'dark',
    transitionAlertMessage: 'fade',
    possitionAlertMessage: 'bottom right',
    logoName: 'Logo.png'
}

export default Constants;