import React from 'react'
import Coin from './Coin/Coin'

const UserCoins = () => {
    return (
        <div className="userCoins">
            Welcome! Here's your money, please drag and drop onto the machine, then select your product.<br/>
            <Coin value={200} />
            <Coin value={100} />
            <Coin value={50} />
            <Coin value={20} />
            <Coin value={10} />
            <Coin value={5} />
            <Coin value={2} />
            <Coin value={1} />
        </div>
    )
}

export default UserCoins;