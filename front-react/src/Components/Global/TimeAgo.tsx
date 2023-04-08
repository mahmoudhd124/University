import React from 'react';
import {formatDistanceToNow, parseISO} from 'date-fns'

const TimeAgo = ({timestamp}: { timestamp: string }) => {
    const date = parseISO(timestamp)
    const timeFromNow = formatDistanceToNow(date)
    const timeAgo = timeFromNow + ' ago'
    return (
        <span title={timestamp}>
            <i>{timeAgo}</i>
        </span>
    );
};

export default TimeAgo;