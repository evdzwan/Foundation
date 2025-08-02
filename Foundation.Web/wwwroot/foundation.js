if (window.foundation === undefined) {
    window.foundation = {
        state: new Map()
    };
}

export const Foundation = {
    deleteState: key => {
        const state = Foundation.getState(key);
        window.foundation.state.delete(key);
        return state;
    },
    getState: key => {
        return window.foundation.state.get(key);
    },
    hasState: key => {
        return window.foundation.state.has(key);
    },
    setState: (key, value) => {
        window.foundation.state.set(key, value);
        return value;
    }
};
