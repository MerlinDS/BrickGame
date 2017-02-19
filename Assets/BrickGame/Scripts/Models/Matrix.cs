// <copyright file="Matrix.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/18/2017 15:16</date>

using System;
using JetBrains.Annotations;
using UnityEngine;

namespace BrickGame.Scripts.Models
{
    /// <summary>
    /// Matrix - base class for rectangular matrix.
    /// <para>
    ///     Matrix can be instantiated with different access rules.
    /// </para>
    /// <seealso cref="IsReadOnly"/>
    /// <seealso cref="IsStrict"/>
    /// </summary>
    /// <typeparam name="T">Type of cells in matrix</typeparam>
    public class Matrix<T>
    {
        //================================       Public Setup       =================================
        /// <summary>
        /// Flag of read only access
        ///
        /// <seealso cref="this[int,int]"/>
        /// </summary>
        public readonly bool IsReadOnly;

        /// <summary>
        /// Flag of access strictness: if flag equals true,
        /// then specified coordinates in an accessor that out of bounds of the matrix will throw and error.
        /// In other cases for coordinates that out of bounds will be returned a default value.
        ///
        /// <para>
        /// For example, if X less then 0 for strict access will throw an error,
        /// for none strict access will return a default value of a matrix type.
        /// </para>
        ///
        /// <seealso cref="this[int,int]"/>
        /// </summary>
        public readonly bool IsStrict;

        /// <summary>
        /// Is matrix nullable
        /// </summary>
        public bool IsNull { get { return _matrix.Length == 0; } }

        /// <summary>
        /// Matrix width, count of cells in row
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Matrix height, count of cells in column
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Access to data in concrete cell
        /// </summary>
        /// <param name="x">X of cell in matrix</param>
        /// <param name="y">y of cell in matrix</param>
        /// <exception cref="IndexOutOfRangeException">Coordinates is out of the matrix bounds.
        /// <see cref="IsStrict"/> flag equals true</exception>
        /// <exception cref="InvalidOperationException">
        /// Matrix has read only access!
        /// <see cref="IsReadOnly"/> flag equals true.
        /// </exception>
        public T this[int x, int y]
        {
            get
            {
                if (x < 0 || y < 0 || x >= Width || y >= Height)
                {
                    if (IsStrict)
                        throw new IndexOutOfRangeException("Coordinates is out of the matrix bounds");
                    return default(T);
                }
                return _matrix[x + y * Width];
            }
            set
            {
                if (IsReadOnly)
                    throw new InvalidOperationException("Matrix has read only access!");
                if (x < 0 || y < 0 || x >= Width || y >= Height)
                {
                    if (IsStrict)
                        throw new IndexOutOfRangeException();
                    return; //Do noting
                }
                _matrix[x + y * Width] = value;
            }
        }

        /// <summary>
        /// Convert matrix to linear array
        /// </summary>
        /// <param name="value">Matrix instance</param>
        /// <returns>An linear array instance</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static implicit operator T[]([NotNull] Matrix<T> value)
        {
            if (value == null) throw new ArgumentNullException("value");

            int length = value.Width * value.Height;
            T[] result = new T[length];
            Array.Copy(value._matrix, result, length);
            return result;
        }
        //================================    Systems properties    =================================
        /// <summary>
        /// Linear representation of the rectangular matrix
        /// </summary>
        private readonly T[] _matrix;

        //================================      Public methods      =================================
        /// <summary>
        /// Copy cells data from source matrix to destination matrix
        /// </summary>
        /// <param name="source">Source matrix</param>
        /// <param name="dest">Destination matrix</param>
        /// <exception cref="ArgumentOutOfRangeException">Destination matrix mast be the same size as souce matrix</exception>
        public static void Copy([NotNull] Matrix<T> source, [NotNull] Matrix<T> dest)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (dest == null) throw new ArgumentNullException("dest");

            if (source.Width != dest.Width || source.Height != dest.Height)
                throw new ArgumentOutOfRangeException("dest",
                    "Destination matrix mast be the same size as souce matrix");

            if(source.Equals(dest))return;

            for (int i = 0; i < source._matrix.Length; i++)
                dest._matrix[i] = source._matrix[i];
        }

        /// <summary>
        /// Create new matrix with data copied from source matrix.
        /// </summary>
        /// <param name="source">Matrix that need to be cloned</param>
        /// <returns>Clone of the source matrix</returns>
        public static Matrix<T> Clone([NotNull] Matrix<T> source)
        {
            return Clone(source, source.IsStrict, source.IsReadOnly);
        }

        /// <summary>
        /// Create new matrix with data copied from source matrix and change assessor's flags in copied matrix
        /// </summary>
        /// <param name="source">Matrix that need to be cloned</param>
        /// <param name="isStrict">Flag of access strictness. Will be replaced in cloned matrix, not in source</param>
        /// <param name="isReadOnly">Flag of read only access. Will be replaced in cloned matrix, not in source</param>
        /// <seealso cref="IsStrict"/>
        /// <seealso cref="IsReadOnly"/>
        /// <returns>Clone of the source matrix</returns>
        // ReSharper disable once MethodOverloadWithOptionalParameter
        public static Matrix<T> Clone([NotNull] Matrix<T> source, bool isStrict = false, bool isReadOnly = false)
        {
            if (source == null) throw new ArgumentNullException("source");
            return new Matrix<T>(source._matrix, source.Width, source.Height, isStrict, isReadOnly);
        }

        /// <summary>
        /// Matrix constructor (Can't be readonly)
        /// </summary>
        /// <param name="width">Matrix width, count of cells in row</param>
        /// <param name="height">Matrix height, count of cells in column</param>
        /// <param name="isStrict">Flag of access strictness.</param>
        /// <seealso cref="IsStrict"/>
        public Matrix(int width, int height, bool isStrict = false) :
            this(new T[width * height], width, height, isStrict, false)
        {
        }
        /// <summary>
        /// Matrix constructor
        /// </summary>
        /// <param name="matrix">Base matrix data</param>
        /// <param name="isStrict">Flag of access strictness.</param>
        /// <param name="isReadOnly">Flag of read only access.</param>
        /// <seealso cref="IsStrict"/>
        /// <seealso cref="IsReadOnly"/>
        /// <exception cref="ArgumentException">Width and height of the matrix can be lass that 1</exception>
        /// <exception cref="ArgumentOutOfRangeException">Size of the matrix doesn't match width * height!</exception>
        public Matrix([NotNull] T[,]matrix, bool isStrict = false, bool isReadOnly = true):
            this(new T[matrix.GetLength(0) * matrix.GetLength(1)], matrix.GetLength(0), matrix.GetLength(1),
                isStrict, isReadOnly)
        {
            for (int x = 0; x < Width; ++x)
            {
                for (int y = 0; y < Height; ++y)
                    _matrix[x + y * Width] = matrix[x, y];
            }
        }

        /// <summary>
        /// Matrix constructor
        /// </summary>
        /// <param name="matrix">Base matrix data</param>
        /// <param name="width">Matrix width, count of cells in row</param>
        /// <param name="height">Matrix height, count of cells in column</param>
        /// <param name="isStrict">Flag of access strictness.</param>
        /// <param name="isReadOnly">Flag of read only access.</param>
        /// <seealso cref="IsStrict"/>
        /// <seealso cref="IsReadOnly"/>
        /// <exception cref="ArgumentOutOfRangeException">Size of the matrix doesn't match width * height!</exception>
        public Matrix([NotNull] T[] matrix, int width, int height, bool isStrict = false, bool isReadOnly = true)
        {
            if (matrix == null) throw new ArgumentNullException("matrix");
            if (matrix.Length != width * height)
                throw new ArgumentOutOfRangeException("matrix", "Size of the matrix doesn't match width * height!");
            _matrix = matrix;
            Width = width;
            Height = height;
            //Set accessors flags
            IsReadOnly = isReadOnly;
            IsStrict = isStrict;
        }

        /// <summary>
        /// Rotate matrix to 90 degrees.
        /// <para>Width and Height of the matrix will be swapped</para>
        /// <param name="clockwise">If true clockwise rotation will happen,
        /// in other cases counterclockwise rotation will happen.</param>
        /// </summary>
        public void Rotate(bool clockwise = true)
        {
            T[] matrix = new T[_matrix.Length]; //temp matrix
            Array.Copy(_matrix, matrix, _matrix.Length);

            for (int i = 0; i < _matrix.Length; ++i)
            {
                int c = RotateCellClockwise(i, Width, Height);
                if (!clockwise) c = _matrix.Length - 1 - c;//flip
                _matrix[c] = matrix[i];
            }
            //Swap width and height
            int temp = Width;
            Width = Height;
            Height = temp;
        }

        /// <summary>
        /// Flipping matrix horizontally.
        /// </summary>
        public void Flip()
        {
            T[] matrix = new T[_matrix.Length]; //temp matrix
            Array.Copy(_matrix, matrix, _matrix.Length);

            for (int i = 0; i < _matrix.Length; ++i)
                _matrix[FlippedCell(i, Width)] = matrix[i];
        }

        /// <summary>
        /// Check if matrix has row with cells of spesified value
        /// </summary>
        /// <param name="row">Index of row</param>
        /// <param name="value">Value of cells ot compare</param>
        /// <returns>True if row contains cells equals value, false in other case</returns>
        /// <exception cref="IndexOutOfRangeException">Row index is out of the matrix bounds</exception>
        public bool RowContainsValue(int row, T value)
        {
            if (row < 0 || row >= Height)
            {
                if (IsStrict)
                    throw new IndexOutOfRangeException("Row index is out of the matrix bounds");
                return false;
            }
            int offset = row * Width;
            for (int x = 0; x < Width; x++)
                if (!_matrix[x + offset].Equals(value)) return false;
            return true;
        }

        /// <summary>
        /// Check for intersection between two matrices
        /// </summary>
        /// <param name="target">Target matrix to interact with</param>
        /// <param name="xOffset">X offset in current matrix</param>
        /// <param name="yOffset">y offset in current matrix</param>
        /// <returns>
        /// True if matrices have a cell that intersects with a cell in target matrix:
        /// cells with same coordinates have values that not equals to default value
        /// I n other cases return false.
        /// </returns>
        public bool HasIntersection([NotNull] Matrix<T> target, int xOffset, int yOffset)
        {
            if (target == null) throw new ArgumentNullException("target");
            int width = target.Width;
            int height = target.Height;
            T @default = default(T);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    T value = target._matrix[x + y * width];
                    if(value.Equals(@default))continue;
                    //target matrix has value in the cell
                    int c = (xOffset + x) + (yOffset + y) * Width;
                    if(c < 0 || c >= _matrix.Length) continue;
                    //the cell is in bounds of current matrix
                    if (_matrix[c].Equals(@default))continue;
                    //current matrix has value in the cell
                    //matrices have an intersection
                    return true;
                }
            }
            //an intersection between matrices was not found
            return false;
        }
        //================================ Equals methods ==========================================
        /// <inheritdoc />
        [ContractAnnotation("null=>false")]
        private bool Equals(Matrix<T> other)
        {
            return Equals(_matrix, other._matrix) && Width == other.Width && Height == other.Height;
        }

        /// <inheritdoc />
        [ContractAnnotation("null=>false")]
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Matrix<T>) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = _matrix != null ? _matrix.GetHashCode() : 0;
                // ReSharper disable NonReadonlyMemberInGetHashCode
                hashCode = (hashCode * 397) ^ Width;
                hashCode = (hashCode * 397) ^ Height;
                // ReSharper restore NonReadonlyMemberInGetHashCode
                return hashCode;
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return "Matrix " + Width + " x " + Height + " " +
                   (IsStrict ? "strict" : "") +
                   (IsReadOnly ? ", readonly " : "") +
                   " of type " + typeof(T);
        }
        //================================ Private|Protected methods ================================
        /// <summary>
        /// Constructor of null object
        /// </summary>
        protected Matrix() : this(0,0)
        {

        }

        /// <summary>
        /// Calculate cell index in linear array that represents a clockwise rotated matrix.
        /// </summary>
        /// <param name="c">current index of cell in a liner array</param>
        /// <param name="w">width of matrix</param>
        /// <param name="h">height of matrix</param>
        /// <returns>index of cell in clockwise  rotated matrix</returns>
        private int RotateCellClockwise(int c, int w, int h)
        {
            int y = c / w;
            return h - 1 - y + (c - y * w) * h;
        }

        /// <summary>
        /// Calculate cell index in linear array that represents horizontally flipped matrix
        /// </summary>
        /// <param name="c">current index of cell in a liner array</param>
        /// <param name="w">width of matrix</param>
        /// <returns>index of cell in horizontally flipped matrix</returns>
        private int FlippedCell(int c, int w)
        {
            int d = w * (c / w);
            return w - 1 - (c - d) + d;
        }
    }
}