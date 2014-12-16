﻿using System;
using System.Collections.Generic;
using System.Xml.Linq;
using TwistedLogik.Nucleus;
using TwistedLogik.Ultraviolet.Content;
using TwistedLogik.Ultraviolet.Graphics.Graphics2D;
using TwistedLogik.Ultraviolet.Layout.Animation;
using TwistedLogik.Ultraviolet.Layout.Stylesheets;

namespace TwistedLogik.Ultraviolet.Layout.Elements
{
    /// <summary>
    /// Represents the top-level container for UI elements.
    /// </summary>
    public sealed class UIView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UIView"/> class.
        /// </summary>
        /// <param name="uv">The Ultraviolet context.</param>
        /// <param name="viewModelType">The view's associated model type.</param>
        public UIView(UltravioletContext uv, Type viewModelType)
        {
            this.viewModelType = viewModelType;

            this.canvas = new Canvas(uv, null);
            this.canvas.UpdateView(this);
        }

        /// <summary>
        /// Loads an instance of <see cref="UIView"/> from an XML document.
        /// </summary>
        /// <param name="uv">The Ultraviolet context.</param>
        /// <param name="xml">The <see cref="XDocument"/> from which to load the view.</param>
        /// <returns>The <see cref="UIView"/> that was loaded from the specified XML document.</returns>
        public static UIView Load(UltravioletContext uv, XDocument xml)
        {
            Contract.Require(uv, "uv");
            Contract.Require(xml, "xml");

            return UIViewLoader.Load(uv, xml.Root);
        }

        /// <summary>
        /// Loads an instance of the <see cref="UIView"/> from an XML node.
        /// </summary>
        /// <param name="uv">The Ultraviolet context.</param>
        /// <param name="xml">The <see cref="XElement"/> from which to load the view.</param>
        /// <returns>The <see cref="UIView"/> that was loaded from the specified XML element.</returns>
        public static UIView Load(UltravioletContext uv, XElement xml)
        {
            Contract.Require(uv, "uv");
            Contract.Require(xml, "xml");

            return UIViewLoader.Load(uv, xml);
        }

        /// <summary>
        /// Assigns mouse capture to the specified element.
        /// </summary>
        /// <param name="element">The element to which to assign mouse capture.</param>
        public void CaptureMouse(UIElement element)
        {
            if (Container != null)
            {
                Container.CaptureMouse(element);
            }
        }

        /// <summary>
        /// Releases the mouse from the element that is currently capturing it.
        /// </summary>
        /// <param name="element">The element that is attempting to release mouse capture.</param>
        public void ReleaseMouse(UIElement element)
        {
            if (Container != null)
            {
                Container.ReleaseMouse(element);
            }
        }

        /// <summary>
        /// Converts a position in screen space to a position in view space.
        /// </summary>
        /// <param name="x">The x-coordinate of the screen space position to convert.</param>
        /// <param name="y">The y-coordinate of the screen space position to convert.</param>
        /// <returns>The converted view space position.</returns>
        public Vector2 ScreenPositionToViewPosition(Int32 x, Int32 y)
        {
            return new Vector2(x - Area.X, y - Area.Y);
        }

        /// <summary>
        /// Converts a position in screen space to a position in view space.
        /// </summary>
        /// <param name="position">The screen space position to convert.</param>
        /// <returns>The converted view space position.</returns>
        public Vector2 ScreenPositionToViewPosition(Vector2 position)
        {
            return ScreenPositionToViewPosition((Int32)position.X, (Int32)position.Y);
        }

        /// <summary>
        /// Converts a position in view space to a position in screen space.
        /// </summary>
        /// <param name="x">The x-coordinate of the view space position to convert.</param>
        /// <param name="y">The y-coordinate of the view space position to convert.</param>
        /// <returns>The converted screen space position.</returns>
        public Vector2 ViewPositionToScreenPosition(Int32 x, Int32 y)
        {
            return new Vector2(x + Area.X, y + Area.Y);
        }

        /// <summary>
        /// Converts a position in view space to a position in screen space.
        /// </summary>
        /// <param name="position">The view space position to convert.</param>
        /// <returns>The converted screen space position.</returns>
        public Vector2 ViewPositionToScreenPosition(Vector2 position)
        {
            return ViewPositionToScreenPosition((Int32)position.X, (Int32)position.Y);
        }

        /// <summary>
        /// Gets the element with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier of the element to retrieve.</param>
        /// <returns>The element with the specified identifier, or <c>null</c> if no such element exists.</returns>
        public UIElement GetElementByID(String id)
        {
            UIElement element;
            elementsByID.TryGetValue(id, out element);
            return element;
        }

        /// <summary>
        /// Gets the element within this view at the specified point in view space.
        /// </summary>
        /// <param name="x">The x-coordinate of the position in view space to evaluate.</param>
        /// <param name="y">The y-coordinate of the position in view space to evaluate.</param>
        /// <returns>The element within this view at the specified point in view space, or <c>null</c> if no element exists at that point.</returns>
        public UIElement GetElementAtPoint(Int32 x, Int32 y)
        {
            return Canvas.GetElementAtPoint(x, y);
        }

        /// <summary>
        /// Gets the element within this view at the specified point in view space.
        /// </summary>
        /// <param name="position">The position in view space to evaluate.</param>
        /// <returns>The element within this view at the specified point in view space, or <c>null</c> if no element exists at that point.</returns>
        public UIElement GetElementAtPoint(Vector2 position)
        {
            return GetElementAtPoint((Int32)position.X, (Int32)position.Y);
        }

        /// <summary>
        /// Gets the element within this view at the specified point in screen space.
        /// </summary>
        /// <param name="x">The x-coordinate of the position in screen space to evaluate.</param>
        /// <param name="y">The y-coordinate of the position in screen space to evaluate.</param>
        /// <returns>The element within this view at the specified point in screen space, or <c>null</c> if no element exists at that point.</returns>
        public UIElement GetElementAtScreenPoint(Int32 x, Int32 y)
        {
            var xprime = x - Area.X;
            var yprime = y - Area.Y;
            return GetElementAtPoint(xprime, yprime);
        }

        /// <summary>
        /// Gets the element within this view at the specified point in screen space.
        /// </summary>
        /// <param name="position">The position in screen space to evaluate.</param>
        /// <returns>The element within this view at the specified point in screen space, or <c>null</c> if no element exists at that point.</returns>
        public UIElement GetElementAtScreenPoint(Vector2 position)
        {
            return GetElementAtScreenPoint((Int32)position.X, (Int32)position.Y);
        }

        /// <summary>
        /// Sets the content managers used to load UI assets.
        /// </summary>
        /// <param name="global">The content manager used to load globally-sourced assets.</param>
        /// <param name="local">The content manager used to load locally-sourced assets.</param>
        public void SetContentManagers(ContentManager global, ContentManager local)
        {
            this.globalContent = global;
            this.localContent  = local;

            Canvas.ReloadContent();
        }

        /// <summary>
        /// Sets the view's stylesheet.
        /// </summary>
        /// <param name="stylesheet">The view's stylesheet.</param>
        public void SetStylesheet(UvssDocument stylesheet)
        {
            this.stylesheet = stylesheet;

            if (stylesheet != null)
            {
                stylesheet.ApplyStylesRecursively(Canvas);
            }
            else
            {
                Canvas.ClearStyledValuesRecursive();
            }
        }

        /// <summary>
        /// Sets the view's associated view model.
        /// </summary>
        /// <param name="viewModel">The view's associated view model.</param>
        public void SetViewModel(Object viewModel)
        {
            if (viewModel != null && viewModel.GetType() != viewModelType)
                throw new ArgumentException(LayoutStrings.IncompatibleViewModel.Format(viewModelType));

            this.viewModel = viewModel;
            Canvas.UpdateViewModel(viewModel);
        }

        /// <summary>
        /// Sets the view's area on the screen.
        /// </summary>
        /// <param name="area">The area on the screen that is occupied by the view.</param>
        public void SetViewArea(Rectangle area)
        {
            var newPosition = false;
            var newSize = false;

            if (this.area.X != area.X || this.area.Y != area.Y)
                newPosition = true;

            if (this.area.Width != area.Width || this.area.Height != area.Height)
                newSize = true;

            this.area = area;

            Canvas.CalculatedWidth  = area.Width;
            Canvas.CalculatedHeight = area.Height;

            if (newSize)
            {
                Canvas.PerformLayout();
            }

            if (newSize || newPosition)
            {
                Canvas.UpdateAbsoluteScreenPosition(area.X, area.Y);
            }
        }

        /// <summary>
        /// Requests that a layout be performed during the next call to <see cref="UIElement.Update(UltravioletTime)"/>.
        /// </summary>
        public void RequestLayout()
        {
            Canvas.RequestLayout();
        }

        /// <summary>
        /// Immediately recalculates the layout of the container and all of its children.
        /// </summary>
        public void PerformLayout()
        {
            Canvas.PerformLayout();
        }

        /// <summary>
        /// Loads the specified asset from the global content manager.
        /// </summary>
        /// <typeparam name="TOutput">The type of object being loaded.</typeparam>
        /// <param name="asset">The identifier of the asset to load.</param>
        /// <returns>The asset that was loaded.</returns>
        public TOutput LoadGlobalContent<TOutput>(AssetID asset)
        {
            if (!asset.IsValid)
                return default(TOutput);

            return (globalContent == null) ? default(TOutput) : globalContent.Load<TOutput>(asset);
        }

        /// <summary>
        /// Loads the specified asset from the local content manager.
        /// </summary>
        /// <typeparam name="TOutput">The type of object being loaded.</typeparam>
        /// <param name="asset">The identifier of the asset to load.</param>
        /// <returns>The asset that was loaded.</returns>
        public TOutput LoadLocalContent<TOutput>(AssetID asset)
        {
            if (!asset.IsValid)
                return default(TOutput);

            return (localContent == null) ? default(TOutput) : localContent.Load<TOutput>(asset);
        }

        /// <summary>
        /// Loads the specified sourced asset.
        /// </summary>
        /// <typeparam name="TOutput">The type of object being loaded.</typeparam>
        /// <param name="asset">The identifier of the asset to load.</param>
        /// <returns>The asset that was loaded.</returns>
        public TOutput LoadContent<TOutput>(SourcedVal<AssetID> asset)
        {
            if (!asset.Value.IsValid)
                return default(TOutput);

            switch (asset.Source)
            {
                case AssetSource.Global:
                    return (globalContent == null) ? default(TOutput) : globalContent.Load<TOutput>(asset.Value);
                
                case AssetSource.Local:
                    return (localContent == null) ? default(TOutput) : localContent.Load<TOutput>(asset.Value);

                default:
                    throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Loads the specified image from the global content manager.
        /// </summary>
        /// <param name="image">The identifier of the image to load.</param>
        public void LoadGlobalContent(StretchableImage9 image)
        {
            if (image == null || globalContent == null)
                return;

            image.Load(globalContent);
        }

        /// <summary>
        /// Loads the specified image from the local content manager.
        /// </summary>
        /// <param name="image">The identifier of the image to load.</param>
        public void LoadLocalContent(StretchableImage9 image)
        {
            if (image == null || localContent == null)
                return;

            image.Load(localContent);
        }

        /// <summary>
        /// Loads the specified sourced image.
        /// </summary>
        /// <param name="image">The identifier of the image to load.</param>
        public void LoadContent(SourcedRef<StretchableImage9> image)
        {
            if (image.Value == null)
                return;

            switch (image.Source)
            {
                case AssetSource.Global:
                    if (globalContent != null)
                    {
                        image.Value.Load(globalContent);
                    }
                    break;

                case AssetSource.Local:
                    if (localContent != null)
                    {
                        image.Value.Load(localContent);
                    }
                    break;

                default:
                    throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Searches the view's associated stylesheet for a storyboard with the specified name.
        /// </summary>
        /// <param name="name">The name of the storyboard to retrieve.</param>
        /// <returns>The <see cref="Storyboard"/> with the specified name, or <c>null</c> if the specified storyboard does not exist.</returns>
        public Storyboard FindStoryboard(String name)
        {
            Contract.RequireNotEmpty(name, "name");

            if (Stylesheet != null)
            {
                return Stylesheet.InstantiateStoryboardByName(Canvas.Ultraviolet, name);
            }

            return null;
        }

        /// <summary>
        /// Gets the <see cref="UIViewCollection"/> that contains the view, if any.
        /// </summary>
        public UIViewCollection Container
        {
            get { return container; }
            internal set { container = value; }
        }

        /// <summary>
        /// Gets the content manager used to load globally-sourced assets.
        /// </summary>
        public ContentManager GlobalContent
        {
            get { return globalContent; }
        }

        /// <summary>
        /// Gets the content manager used to load locally-sourced assets.
        /// </summary>
        public ContentManager LocalContent
        {
            get { return localContent; }
        }

        /// <summary>
        /// Gets the stylesheet that is currently applied to this view.
        /// </summary>
        public UvssDocument Stylesheet
        {
            get { return stylesheet; }
        }

        /// <summary>
        /// Gets the view's view model.
        /// </summary>
        public Object ViewModel
        {
            get { return viewModel; }
        }

        /// <summary>
        /// Gets the type of view model expected by this view.
        /// </summary>
        public Type ViewModelType
        {
            get { return viewModelType; }
        }

        /// <summary>
        /// Gets or sets the area on the screen that the UI view occupies.
        /// </summary>
        public Rectangle Area
        {
            get { return area; }
            set
            {
                if (!area.Equals(value))
                {
                    area = value;
                    Canvas.ContainerRelativeLayout = new Rectangle(0, 0, value.Width, value.Height);
                    Canvas.PerformLayout();
                }
            }
        }

        /// <summary>
        /// Gets the x-coordinate of the view's top left corner.
        /// </summary>
        public Int32 X
        {
            get { return area.X; }
        }

        /// <summary>
        /// Gets the y-coordinate of the view's top left corner.
        /// </summary>
        public Int32 Y
        {
            get { return area.Y; }
        }

        /// <summary>
        /// Gets the view's width on the screen.
        /// </summary>
        public Int32 Width
        {
            get { return area.Width; }
        }

        /// <summary>
        /// Gets the view's height on the screen.
        /// </summary>
        public Int32 Height
        {
            get { return area.Height; }
        }

        /// <summary>
        /// Gets the <see cref="Canvas"/> that contains all of the UI's elements.
        /// </summary>
        public Canvas Canvas
        {
            get { return canvas; }
        }

        /// <summary>
        /// Draws the view and all of its contained elements.
        /// </summary>
        /// <param name="time">Time elapsed since the last call to <see cref="UltravioletContext.Draw(UltravioletTime)"/>.</param>
        /// <param name="spriteBatch">The sprite batch with which to draw the view.</param>
        internal void Draw(UltravioletTime time, SpriteBatch spriteBatch)
        {
            Canvas.Draw(time, spriteBatch);
        }

        /// <summary>
        /// Updates the view's state and the state of its contained elements.
        /// </summary>
        /// <param name="time">Time elapsed since the last call to <see cref="UltravioletContext.Draw(UltravioletTime)"/>.</param>
        internal void Update(UltravioletTime time)
        {
            Canvas.Update(time);
        }

        /// <summary>
        /// Registers an element with the view's identifier cache.
        /// </summary>
        /// <param name="element">The element to register.</param>
        internal void RegisterElementID(UIElement element)
        {
            if (String.IsNullOrEmpty(element.ID))
                return;

            UIElement existing;
            if (elementsByID.TryGetValue(element.ID, out existing))
                throw new InvalidOperationException(LayoutStrings.ElementWithIDAlreadyExists.Format(element.ID));

            elementsByID[element.ID] = element;
        }

        /// <summary>
        /// Removes an element from the view's identifier cache.
        /// </summary>
        /// <param name="element">The element to remove.</param>
        internal void UnregisterElementID(UIElement element)
        {
            if (String.IsNullOrEmpty(element.ID))
                return;

            UIElement existing;
            elementsByID.TryGetValue(element.ID, out existing);

            if (existing == element)
                elementsByID.Remove(element.ID);
        }

        // Property values.
        private UIViewCollection container;
        private ContentManager globalContent;
        private ContentManager localContent;
        private UvssDocument stylesheet;
        private Object viewModel;
        private readonly Type viewModelType;
        private Rectangle area;
        private Canvas canvas;

        // A dictionary which associates element IDs with elements.
        private readonly Dictionary<String, UIElement> elementsByID = 
            new Dictionary<String, UIElement>(StringComparer.InvariantCultureIgnoreCase);
    }
}
