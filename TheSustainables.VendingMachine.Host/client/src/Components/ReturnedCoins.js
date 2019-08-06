import React from 'react'
import Coin from './Coin/Coin'
import { Button } from 'reactstrap'
import './ReturnedCoins.css'

const ReturnedCoins = (props) => {
    let title = props.returnedCoins.length > 0 ? <div> Here's your change (<Button color="link" className="closeCoins" onClick={props.close}>Close</Button>)</div> : "";
    return (
        <div className="returnedCoins">
            {title}
            {props.returnedCoins.map((coin, i) =>
                <Coin style={{ float: "left" }} key={i} value={coin.value} />
            )}
        </div>
    )
}

export default ReturnedCoins;