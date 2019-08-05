import React from 'react'

export default (props) => {
    return (
        <div className="product">
            <b>{props.name}</b> <i>{props.price}</i>
        </div>
    )
}