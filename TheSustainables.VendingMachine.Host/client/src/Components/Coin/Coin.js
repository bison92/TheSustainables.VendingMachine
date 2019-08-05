import React from 'react';
import './Coin.css';
const coin = (props) => {
    let className = "coin";
    switch (props.value) {
        case 200:
            className += " coin_2_euro"
            break
        case 100:
            className += " coin_1_euro"
            break
        case 50: 
            className += " coin_50_cent"
            break
        case 20:
            className += " coin_20_cent"
            break
        case 10:
            className += " coin_10_cent"
            break
        case 5:
            className += " coin_5_cent"
            break
        case 2:
            className += " coin_2_cent"
            break
        case 1:
            className += " coin_1_cent"
            break
    }
    return (<div role="coin" className={className}></div>)
}

export default coin