import React from 'react'

import DraggableCoin from './DragAndDrop/DraggableCoin';

const UserCoins = (props) => {
    return (
        <div className="userCoins">
            Welcome! Here's your money, please drag and drop onto the machine, then select your product.<br />
            <DraggableCoin value={100} updateCredit={props.updateCredit} />
            <DraggableCoin value={50} updateCredit={props.updateCredit} />
            <DraggableCoin value={20} updateCredit={props.updateCredit} />
            <DraggableCoin value={10} updateCredit={props.updateCredit} />
        </div>
    )
}

export default UserCoins;