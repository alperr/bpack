function bpack(size)
{
	this.buf = new Buffer(size);
	this.offset = 0;
}

bpack.prototype.write_int32 = function(val)
{
	this.buf.writeInt32LE(val, this.offset);
	this.offset += 4;
};

bpack.prototype.read_int32 = function(buffer, offset)
{
	return buffer.readInt32LE(offset);
};

bpack.prototype.write_uint32 = function(val)
{
	this.buf.writeUInt32LE(val, this.offset);
	this.offset += 4;
};

bpack.prototype.read_uint32 = function(buffer, offset)
{
	return buffer.readUInt32LE(offset);
};

bpack.prototype.write_int16 = function(val)
{
	this.buf.writeInt16LE(val, this.offset);
	this.offset += 2;
};

bpack.prototype.read_int16 = function(buffer, offset)
{
	return buffer.readInt16LE(offset);
};

bpack.prototype.write_uint16 = function(val)
{
	this.buf.writeUInt16LE(val, this.offset);
	this.offset += 2;
};

bpack.prototype.read_uint16 = function(buffer, offset)
{
	return buffer.readUInt16LE(offset);
};


bpack.prototype.write_uint8 = function(val)
{
	this.buf.writeUInt8(val, this.offset);
	this.offset += 1;
};

bpack.prototype.read_uint8 = function(buffer, offset)
{
	return buffer.readUInt8(offset);
};

bpack.prototype.write_float = function(val)
{
	this.buf.writeFloatLE(val, this.offset);
	this.offset += 4;
};

bpack.prototype.read_float = function(buffer, offset)
{
	return buffer.readFloatLE(offset);
};

bpack.prototype.write_double = function(val)
{
	this.buf.writeDoubleLE(val, this.offset);
	this.offset += 8;
};

bpack.prototype.read_double = function(buffer, offset)
{
	return buffer.readDoubleLE(offset);
};

bpack.prototype.write_string = function(val)
{
	var len = Buffer.byteLength(val, 'utf8');
	this.buf.writeInt32LE(len, this.offset);
	this.offset += 4;
	this.buf.write(val, this.offset, 'utf8');
	this.offset += len;
};

bpack.prototype.read_string = function(buffer, offset)
{
	var len = buffer.readInt32LE(offset);
	offset += 4;
	return buffer.toString('utf8', offset, offset + len);
};

bpack.prototype.write_buffer = function(buffer)
{
	this.buf.writeInt32LE(buffer.length, this.offset);
	this.offset += 4;
	buffer.copy(this.buf, this.offset);
	this.offset += buffer.length;
};

bpack.prototype.read_buffer = function(buffer, offset)
{
	var len = buffer.readInt32LE(offset);
	offset += 4;
	return buffer.slice(offset, offset + len);
};

bpack.prototype.write_buffer_array = function(arr)
{
	this.buf.writeInt32LE(arr.length, this.offset);
	this.offset += 4;
	for (var i = 0; i < arr.length; i++)
	{
		this.write_buffer(arr[i]);
	}
};

bpack.prototype.read_buffer_array = function(buffer, offset)
{
	var len = buffer.readInt32LE(offset);
	offset += 4;
	var result = [];
	for (var i = 0; i < len; i++)
	{
		result.push(this.read_buffer(buffer, offset));
		offset += result[i].length + 4;
	}
	return result;
};

bpack.prototype.write_int32_array = function(arr)
{
	this.buf.writeInt32LE(arr.length, this.offset);
	this.offset += 4;
	for (var i = 0; i < arr.length; i++)
	{
		this.write_int32(arr[i]);
	}
};

bpack.prototype.read_int32_array = function(buffer, offset)
{
	var len = buffer.readInt32LE(offset);
	offset += 4;
	var result = [];
	for (var i = 0; i < len; i++)
	{
		result.push(this.read_int32(buffer, offset));
		offset += 4;
	}
	return result;
};

bpack.prototype.write_uint32_array = function(arr)
{
	this.buf.writeInt32LE(arr.length, this.offset);
	this.offset += 4;
	for (var i = 0; i < arr.length; i++)
	{
		this.write_uint32(arr[i]);
	}
};

bpack.prototype.read_uint32_array = function(buffer, offset)
{
	var len = buffer.readInt32LE(offset);
	offset += 4;
	var result = [];
	for (var i = 0; i < len; i++)
	{
		result.push(this.read_uint32(buffer, offset));
		offset += 4;
	}
	return result;
};

bpack.prototype.write_int16_array = function(arr)
{
	this.buf.writeInt32LE(arr.length, this.offset);
	this.offset += 4;
	for (var i = 0; i < arr.length; i++)
	{
		this.write_int16(arr[i]);
	}
};

bpack.prototype.read_int16_array = function(buffer, offset)
{
	var len = buffer.readInt32LE(offset);
	offset += 4;
	var result = [];
	for (var i = 0; i < len; i++)
	{
		result.push(this.read_int16(buffer, offset));
		offset += 2;
	}
	return result;
};

bpack.prototype.write_uint16_array = function(arr)
{
	this.buf.writeInt32LE(arr.length, this.offset);
	this.offset += 4;
	for (var i = 0; i < arr.length; i++)
	{
		this.write_uint16(arr[i]);
	}
};

bpack.prototype.read_uint16_array = function(buffer, offset)
{
	var len = buffer.readInt32LE(offset);
	offset += 4;
	var result = [];
	for (var i = 0; i < len; i++)
	{
		result.push(this.read_uint16(buffer, offset));
		offset += 2;
	}
	return result;
};

bpack.prototype.write_uint8_array = function(arr)
{
	this.buf.writeInt32LE(arr.length, this.offset);
	this.offset += 4;
	for (var i = 0; i < arr.length; i++)
	{
		this.write_uint8(arr[i]);
	}
};

bpack.prototype.read_uint8_array = function(buffer, offset)
{
	var len = buffer.readInt32LE(offset);
	offset += 4;
	var result = [];
	for (var i = 0; i < len; i++)
	{
		result.push(this.read_uint8(buffer, offset));
		offset += 1;
	}
	return result;
};

bpack.prototype.write_float_array = function(arr)
{
	this.buf.writeInt32LE(arr.length, this.offset);
	this.offset += 4;
	for (var i = 0; i < arr.length; i++)
	{
		this.write_float(arr[i]);
	}
};

bpack.prototype.read_float_array = function(buffer, offset)
{
	var len = buffer.readInt32LE(offset);
	offset += 4;
	var result = [];
	for (var i = 0; i < len; i++)
	{
		result.push(this.read_float(buffer, offset));
		offset += 4;
	}
	return result;
};

bpack.prototype.write_double_array = function(arr)
{
	this.buf.writeInt32LE(arr.length, this.offset);
	this.offset += 4;
	for (var i = 0; i < arr.length; i++)
	{
		this.write_double(arr[i]);
	}
};

bpack.prototype.read_float_array = function(buffer, offset)
{
	var len = buffer.readInt32LE(offset);
	offset += 4;
	var result = [];
	for (var i = 0; i < len; i++)
	{
		result.push(this.read_double(buffer, offset));
		offset += 8;
	}
	return result;
};

bpack.prototype.write_string_array = function(arr)
{
	this.buf.writeInt32LE(arr.length, this.offset);
	this.offset += 4;
	for (var i = 0; i < arr.length; i++)
	{
		this.write_string(arr[i]);
	}
};

bpack.prototype.read_string_array = function(buffer, offset)
{
	var len = buffer.readInt32LE(offset);
	offset += 4;
	var result = [];
	for (var i = 0; i < len; i++)
	{
		var str = this.read_string(buffer, offset);
		result.push(str);
		offset += 4 + Buffer.byteLength(str, 'utf8');
	}
	return result;
};



module.exports = bpack;
