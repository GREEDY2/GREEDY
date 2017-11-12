import * as React from 'react';
import AlertContainer from 'react-alert'

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
        offset: 15,
        position: 'bottom right',
        theme: 'dark',
        time: 5000,
        transition: 'fade'
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