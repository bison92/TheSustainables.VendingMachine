import React from 'react'
import './Product.css'

export default (props) => {
    let className = "product"
    return (
        <div onClick={props.onClick} className={className}>
            <b>{props.name}</b> <i>{props.price}</i>
        </div>
    )
}