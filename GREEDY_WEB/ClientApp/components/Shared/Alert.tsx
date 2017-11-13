import * as React from 'react';
import AlertContainer from 'react-alert'
import Constants from './Constants';

interface Props {
    onRef: any
}

export class Alert extends React.Component<Props> {
    msg: any;

    componentDidMount() {
        this.props.onRef(this);
    }

    componentWillUnmount() {
        this.props.onRef(undefined);
    }

    alertOptions = {
        offset: Constants.offsetAlertMessage,
        position: Constants.possitionAlertMessage,
        theme: Constants.themeAlertMessage,
        time: Constants.displayAlertMessage,
        transition: Constants.transitionAlertMessage
    }

    showAlert = (message, type) => {
        this.msg.show(message, {
            type
        })
    }

    render() {
        return (
            <AlertContainer ref={a => this.msg = a} {...this.alertOptions} />
        )
    }
}